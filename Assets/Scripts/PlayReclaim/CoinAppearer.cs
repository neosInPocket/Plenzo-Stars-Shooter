using UnityEngine;

public class CoinAppearer : MonoBehaviour
{
	[SerializeField] private CoinTraveler startCoin;
	[SerializeField] private CoinTraveler coinPrefabInstance;
	[SerializeField] private GameObject top;
	[SerializeField] private GameObject bottom;
	[SerializeField] private GameObject connector;
	[SerializeField] private float topSize;
	[SerializeField] private float bottomSize;
	[SerializeField] private float connectorSize;
	[SerializeField] private float ySpawnValue;

	private void Start()
	{
		startCoin.CoinCollected += CoinCollected;
	}

	private void CoinCollected(CoinTraveler coinTraveler)
	{
		coinTraveler.CoinCollected -= CoinCollected;

		var randomNumber = Random.Range(0, 3);
		Vector3 spawnPosition = default;

		if (randomNumber == 0)
		{
			spawnPosition = new Vector3(
				top.transform.position.x,
				ySpawnValue,
				Random.Range(top.transform.position.z - topSize / 2, top.transform.position.z + topSize / 2)
				);
		}

		if (randomNumber == 1)
		{
			spawnPosition = new Vector3(
							bottom.transform.position.x,
							ySpawnValue,
							Random.Range(bottom.transform.position.z - bottomSize / 2, bottom.transform.position.z + bottomSize / 2)
							);
		}

		if (randomNumber == 2)
		{
			spawnPosition = new Vector3(
							connector.transform.position.x,
							ySpawnValue,
							3.9f
							);
		}

		var newCoin = Instantiate(coinPrefabInstance, spawnPosition, Quaternion.identity, transform);
		newCoin.CoinCollected += CoinCollected;
	}
}
