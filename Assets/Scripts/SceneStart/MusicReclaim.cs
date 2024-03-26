using System.Linq;
using UnityEngine;

public class MusicReclaim : MonoBehaviour
{
	[SerializeField] private AudioSource musicAudio;
	private Reclaimer reclaimer;
	public Reclaimer Reclaimer => reclaimer;

	private void Awake()
	{
		reclaimer = GameObject.FindObjectOfType<Reclaimer>();

		MusicReclaim[] reclaimers = FindObjectsByType<MusicReclaim>(sortMode: FindObjectsSortMode.None);
		var length = reclaimers.Length == 1;

		if (!length)
		{
			var reclaimer = reclaimers.FirstOrDefault(x => x.gameObject.scene.name != "DontDestroyOnLoad");
			Destroy(reclaimer.gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		musicAudio.enabled = reclaimer.reclaimData.reclaimVolume;
	}

	public bool ManipulateMusicValue()
	{
		musicAudio.enabled = !reclaimer.reclaimData.reclaimVolume;
		reclaimer.reclaimData.reclaimVolume = musicAudio.enabled;
		reclaimer.SaveReclaim();

		return musicAudio.enabled;
	}

	public bool ReclaimSoundEffect()
	{
		reclaimer.reclaimData.reclaimSoundEffect = !reclaimer.reclaimData.reclaimSoundEffect;

		reclaimer.SaveReclaim();

		return reclaimer.reclaimData.reclaimSoundEffect;
	}
}
