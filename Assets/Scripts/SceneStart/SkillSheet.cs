using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSheet : MonoBehaviour
{
	[SerializeField] private Image skillimage;
	[SerializeField] private Button skillButton;
	[SerializeField] private TMP_Text skillPointsCost;
	[SerializeField] private TMP_Text skillButtonText;
	[SerializeField] private int skillIndex;
	[SerializeField] private int skillPointsRequired;
	[SerializeField] private Color noUpgraded;
	[SerializeField] private Color rejected;
	[SerializeField] private Color upgraded;
	[SerializeField] private SkillSheet otherSheet;
	[SerializeField] private Reclaimer reclaimer;
	[SerializeField] private SkillPointsRefresher[] refreshers;

	private void Start()
	{
		SetSkillValues();
	}

	public void SetSkillValues()
	{
		skillPointsCost.text = skillPointsRequired.ToString();
		bool skillUpgraded = false;
		bool skillPointsEnough = reclaimer.reclaimData.reclaimSkillPoints >= skillPointsRequired;

		if (skillIndex == 0)
		{
			skillUpgraded = reclaimer.reclaimData.reclaimEffect1;
		}
		else
		{
			skillUpgraded = reclaimer.reclaimData.reclaimEffect2;
		}

		Color imageColor;
		Color buttonTextColor;
		string buttonText;
		bool interactable;

		if (skillUpgraded)
		{
			imageColor = Color.white;
			buttonTextColor = upgraded;
			buttonText = "UPGRADED";
			interactable = false;
		}
		else
		{
			imageColor = Color.black;

			if (skillPointsEnough)
			{
				buttonTextColor = noUpgraded;
				buttonText = "UPGRADE";
				interactable = true;
			}
			else
			{
				buttonTextColor = rejected;
				buttonText = "NO SKILL POINTS";
				interactable = false;
			}
		}

		skillimage.color = imageColor;
		skillButtonText.text = buttonText;
		skillButton.interactable = interactable;
		skillButtonText.color = buttonTextColor;
	}

	public void UpgradeSkill()
	{
		reclaimer.reclaimData.reclaimSkillPoints -= skillPointsRequired;
		if (skillIndex == 0)
		{
			reclaimer.reclaimData.reclaimEffect1 = true;
		}
		else
		{
			reclaimer.reclaimData.reclaimEffect2 = true;
		}

		reclaimer.SaveReclaim();

		for (int i = 0; i < refreshers.Length; i++)
		{
			refreshers[i].RefreshSkillPoints();
		}

		otherSheet.SetSkillValues();
		SetSkillValues();
	}
}
