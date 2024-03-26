using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
	[SerializeField] private Image progressImage;
	[SerializeField] private TMP_Text progressText;
	[SerializeField] private TMP_Text levelReclaimer;
	[SerializeField] private Reclaimer reclaimer;
	public int MaxData { get; set; }
	public int LevelReclaim { get; set; }
	public int CurrentReclaim { get; set; }

	private void Start()
	{
		ReclaimLevelData();

		progressImage.fillAmount = 0;
		progressText.text = "0/" + MaxData;
		levelReclaimer.text = "level " + reclaimer.reclaimData.reclaimLevel.ToString();
	}

	public void ReclaimUpdate()
	{
		progressImage.fillAmount = (float)CurrentReclaim / (float)MaxData;
		progressText.text = CurrentReclaim + "/" + MaxData;
	}

	private void ReclaimLevelData()
	{
		var level = reclaimer.reclaimData.reclaimLevel;
		MaxData = 3 * level;
		LevelReclaim = 13 * (int)Mathf.Sqrt(level);
	}

	public bool CheckCurrentReclaim()
	{
		ReclaimUpdate();

		if (CurrentReclaim >= MaxData)
		{
			CurrentReclaim = MaxData;
			return true;
		}
		else
		{
			return false;
		}
	}
}
