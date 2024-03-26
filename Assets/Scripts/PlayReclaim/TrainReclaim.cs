using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TrainReclaim : MonoBehaviour
{
	[SerializeField] private TMP_Text pulseText;
	[SerializeField] private Animator pulseArrow;
	[SerializeField] private Reclaimer reclaimer;
	public Action Reclaimed { get; set; }
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public bool TrainNeed()
	{
		if (reclaimer.reclaimData.reclaimTutorial)
		{
			reclaimer.reclaimData.reclaimTutorial = false;
			reclaimer.SaveReclaim();
			StartTrainReclaim();
			return true;
		}
		else
		{
			return false;
		}
	}

	public void StartTrainReclaim()
	{
		this.gameObject.SetActive(true);
		Touch.onFingerDown += BallControl;
		pulseText.text = "WELCOME TO PLINKOBILITY!";
	}

	private void BallControl(Finger finger)
	{
		Touch.onFingerDown -= BallControl;
		Touch.onFingerDown += BewareBlocks;
		pulseText.text = "THIS IS YOU BALL! CONTROL IT BY SWIPING YOUR SCREEN IN DIFFERENT DIRECTIONS";
		pulseArrow.gameObject.SetActive(true);
	}

	private void BewareBlocks(Finger finger)
	{
		Touch.onFingerDown -= BewareBlocks;
		Touch.onFingerDown += MessUp;
		pulseText.text = "Beware of the oncoming blocks that may knock your ball off the path!";
		pulseArrow.SetTrigger("danger");
	}

	private void MessUp(Finger finger)
	{
		Touch.onFingerDown -= MessUp;
		Touch.onFingerDown += CollectCoins;
		pulseText.text = "Don't mess up the controls of your ball! if you fly off the path, the level will end";
		pulseArrow.SetTrigger("destroy");
	}

	private void CollectCoins(Finger finger)
	{
		Touch.onFingerDown -= CollectCoins;
		Touch.onFingerDown += ReclaimedPhase;
		pulseText.text = "COLLECT COINS TO COMPLETE LEVEL! GOOD LUCK!";
		pulseArrow.SetTrigger("coins");
	}

	private void ReclaimedPhase(Finger finger)
	{
		Touch.onFingerDown -= ReclaimedPhase;
		Reclaimed();

		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}
}
