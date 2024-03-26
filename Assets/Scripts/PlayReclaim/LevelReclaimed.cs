using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReclaimed : MonoBehaviour
{
	[SerializeField] private TMP_Text reclaimResult;
	[SerializeField] private GameObject rewardSheet;
	[SerializeField] private GameObject loseSheet;
	[SerializeField] private TMP_Text rewardText;
	[SerializeField] private TMP_Text nextLevelButtonText;
	[SerializeField] private Reclaimer reclaimer;

	public int Reward { get; set; }

	public void ReclaimResult(bool reclaimed)
	{
		if (reclaimed)
		{
			reclaimResult.text = "LEVEL COMPLETED!";
			loseSheet.gameObject.SetActive(false);
			rewardText.text = Reward.ToString();
			nextLevelButtonText.text = "NEXT LEVEl";
			reclaimer.reclaimData.reclaimLevel++;
			reclaimer.reclaimData.reclaimSkillPoints += Reward;
			reclaimer.SaveReclaim();
		}
		else
		{
			reclaimResult.text = "YOU LOSE";
			rewardSheet.gameObject.SetActive(false);
			nextLevelButtonText.text = "try again";
		}

		gameObject.SetActive(true);
	}

	public void Exit()
	{
		SceneManager.LoadScene("MenuReclaim");
	}

	public void Play()
	{
		SceneManager.LoadScene("PlayReclaim");
	}
}
