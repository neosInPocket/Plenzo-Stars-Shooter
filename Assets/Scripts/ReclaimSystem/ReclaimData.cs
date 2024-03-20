
using System;

[Serializable]
public class ReclaimData : IDataClonable
{
	public int reclaimLevel;
	public int reclaimSkillPoints;
	public bool reclaimEffect1;
	public bool reclaimEffect2;
	public bool reclaimVolume;
	public bool reclaimSoundEffect;
	public bool reclaimTutorial;

	public ReclaimData CloneData()
	{
		var reclaim = new ReclaimData();
		reclaim.reclaimLevel = reclaimLevel;
		reclaim.reclaimSkillPoints = reclaimSkillPoints;
		reclaim.reclaimVolume = reclaimVolume;
		reclaim.reclaimSoundEffect = reclaimSoundEffect;
		reclaim.reclaimTutorial = reclaimTutorial;
		reclaim.reclaimEffect2 = reclaimEffect2;
		reclaim.reclaimEffect1 = reclaimEffect1;
		return reclaim;
	}
}
