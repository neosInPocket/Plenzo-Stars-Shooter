using System.Collections;
using UnityEngine;

public class PuzzlesSpawnSystem : MonoBehaviour
{
	[SerializeField] private Puzzle[] prefabs;
	[SerializeField] private Vector2 xySpawnDistances;
	[SerializeField] private Vector2 xySpawnDelays;
	[SerializeField] private Vector2 zSpawnRanges;
	[SerializeField] private Vector2 velocitiesRanges;
	[SerializeField] private Reclaimer reclaimer;
	[SerializeField] private AnimationCurve spawnCurve;
	public bool Started { get; set; }

	public void StartSpawn()
	{
		Started = true;
		SpawnAction();
	}

	private void SpawnAction()
	{
		if (!Started) return;
		StartCoroutine(SpawnTimer());
	}

	private IEnumerator SpawnTimer()
	{
		SpawnPuzzle();
		var randomSpawn = Random.Range(xySpawnDelays.x, xySpawnDelays.y);
		yield return new WaitForSeconds(randomSpawn);
		SpawnAction();
	}

	private void SpawnPuzzle()
	{
		Puzzle randomPrefab = prefabs[Random.Range(0, prefabs.Length)];
		var zSpawn = Random.Range(zSpawnRanges.x, zSpawnRanges.y);
		var xSpawn = spawnCurve.Evaluate(zSpawn);
		Vector3 prefabSpawnPosition = new Vector3(xSpawn, xySpawnDistances.y, zSpawn);
		var puzzle = Instantiate(randomPrefab, prefabSpawnPosition, Quaternion.identity, transform);
		puzzle.StartRoute(reclaimer.reclaimData.reclaimEffect2 ? velocitiesRanges.y : velocitiesRanges.x);
	}
}
