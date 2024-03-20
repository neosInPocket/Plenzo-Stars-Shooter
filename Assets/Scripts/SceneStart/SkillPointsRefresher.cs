using TMPro;
using UnityEngine;

public class SkillPointsRefresher : MonoBehaviour
{
	[SerializeField] private TMP_Text refresher;
	[SerializeField] private Reclaimer reclaimer;
	private int count
	{
		get => 0;
		set
		{
			refresher.text = value.ToString();
		}
	}

	private void Start()
	{
		RefreshSkillPoints();
	}

	public void RefreshSkillPoints()
	{
		count = reclaimer.reclaimData.reclaimSkillPoints;
	}
}
