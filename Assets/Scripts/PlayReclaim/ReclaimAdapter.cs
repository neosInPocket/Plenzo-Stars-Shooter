using TMPro;
using UnityEngine;

public class ReclaimAdapter : MonoBehaviour
{
	[SerializeField] private PlayerTraveler playerTraveler;
	[SerializeField] private PuzzlesSpawnSystem puzzlesSpawnSystem;
	[SerializeField] private Status status;
	[SerializeField] private TrainReclaim trainReclaim;
	[SerializeField] private WaitSheet waitSheet;
	[SerializeField] private LevelReclaimed levelReclaimed;

	private void Start()
	{
		if (trainReclaim.TrainNeed())
		{
			trainReclaim.Reclaimed += OnReclaimed;
		}
		else
		{
			OnReclaimed();
		}
	}

	private void OnReclaimed()
	{
		trainReclaim.Reclaimed -= OnReclaimed;
		waitSheet.Awaited += InputHandler;
		waitSheet.WaitForInput();
	}

	private void InputHandler()
	{
		waitSheet.Awaited -= InputHandler;
		playerTraveler.SetPlayer();
		puzzlesSpawnSystem.StartSpawn();

		playerTraveler.Slipped += PlayerSlipped;
		playerTraveler.CoinHandler += CoinCallback;
	}

	public void PlayerSlipped()
	{
		playerTraveler.Slipped -= PlayerSlipped;
		playerTraveler.CoinHandler -= CoinCallback;
		levelReclaimed.ReclaimResult(false);
	}

	public void CoinCallback()
	{
		status.CurrentReclaim++;

		if (status.CheckCurrentReclaim())
		{
			playerTraveler.Slipped -= PlayerSlipped;
			playerTraveler.CoinHandler -= CoinCallback;
			playerTraveler.PlayerWin();


			levelReclaimed.Reward = status.LevelReclaim;
			levelReclaimed.ReclaimResult(true);
		}
	}

	private void OnDestroy()
	{
		trainReclaim.Reclaimed -= OnReclaimed;
		waitSheet.Awaited -= InputHandler;

		playerTraveler.Slipped -= PlayerSlipped;
		playerTraveler.CoinHandler -= CoinCallback;
	}
}
