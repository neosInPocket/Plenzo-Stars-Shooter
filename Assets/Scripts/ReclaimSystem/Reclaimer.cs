using System;
using UnityEngine;

public class Reclaimer : MonoBehaviour
{
	[SerializeField] private bool reclaimNewData;
	[SerializeField] private ReclaimData defaultReclaim;
	public ReclaimData reclaimData { get; private set; }

	private void Awake()
	{
		if (reclaimNewData)
		{
			reclaimData = defaultReclaim.CloneData();
			SaveReclaim();
		}
		else
		{
			Reclaim();
		}
	}

	public void SaveReclaim()
	{
		PlayerPrefs.SetInt("reclaimLevel", reclaimData.reclaimLevel);
		PlayerPrefs.SetInt("reclaimSkillPoints", reclaimData.reclaimSkillPoints);
		PlayerPrefs.SetInt("reclaimEffect1", Convert.ToInt32(reclaimData.reclaimEffect1));
		PlayerPrefs.SetInt("reclaimEffect2", Convert.ToInt32(reclaimData.reclaimEffect2));
		PlayerPrefs.SetInt("reclaimVolume", Convert.ToInt32(reclaimData.reclaimVolume));
		PlayerPrefs.SetInt("reclaimSoundEffect", Convert.ToInt32(reclaimData.reclaimSoundEffect));
		PlayerPrefs.SetInt("reclaimTutorial", Convert.ToInt32(reclaimData.reclaimTutorial));
	}

	private void Reclaim()
	{
		reclaimData = new ReclaimData();
		reclaimData.reclaimLevel = PlayerPrefs.GetInt("reclaimLevel", defaultReclaim.reclaimLevel);
		reclaimData.reclaimSkillPoints = PlayerPrefs.GetInt("reclaimSkillPoints", defaultReclaim.reclaimSkillPoints);
		reclaimData.reclaimEffect1 = Convert.ToBoolean(PlayerPrefs.GetInt("reclaimEffect1", Convert.ToInt32(defaultReclaim.reclaimEffect1)));
		reclaimData.reclaimEffect2 = Convert.ToBoolean(PlayerPrefs.GetInt("reclaimEffect2", Convert.ToInt32(defaultReclaim.reclaimEffect2)));
		reclaimData.reclaimVolume = Convert.ToBoolean(PlayerPrefs.GetInt("reclaimVolume", Convert.ToInt32(defaultReclaim.reclaimVolume)));
		reclaimData.reclaimSoundEffect = Convert.ToBoolean(PlayerPrefs.GetInt("reclaimSoundEffect", Convert.ToInt32(defaultReclaim.reclaimSoundEffect)));
		reclaimData.reclaimTutorial = Convert.ToBoolean(PlayerPrefs.GetInt("reclaimTutorial", Convert.ToInt32(defaultReclaim.reclaimTutorial)));
	}
}
