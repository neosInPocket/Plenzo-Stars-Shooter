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

		if (gameObject.scene.name == "DontDestroyOnLoad")
		{
			var allReclaimers = GameObject.FindObjectsOfType<MusicReclaim>();
			var nonReclaimer = allReclaimers.FirstOrDefault(x => x.gameObject != gameObject);
			Destroy(nonReclaimer.gameObject);
		}

		DontDestroyOnLoad(gameObject);
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
