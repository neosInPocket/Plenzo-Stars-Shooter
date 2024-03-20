using UnityEngine;
using UnityEngine.UI;

public class ReclaimOptions : MonoBehaviour
{
	[SerializeField] private Image soundsButton;
	[SerializeField] private Image mueisButton;
	[SerializeField] private Color reclaimerColor;
	private MusicReclaim musicReclaim;

	private void Start()
	{
		musicReclaim = GameObject.FindObjectOfType<MusicReclaim>();

		bool soundsEnabled = musicReclaim.Reclaimer.reclaimData.reclaimSoundEffect;
		bool musicAudio = musicReclaim.Reclaimer.reclaimData.reclaimVolume;

		if (soundsEnabled)
		{
			soundsButton.color = Color.white;
		}
		else
		{
			soundsButton.color = reclaimerColor;
		}

		if (musicAudio)
		{
			mueisButton.color = Color.white;
		}
		else
		{
			mueisButton.color = reclaimerColor;
		}
	}

	public void ButtonSounds()
	{
		bool soundsEnabled = musicReclaim.ReclaimSoundEffect();

		if (!soundsEnabled)
		{
			soundsButton.color = reclaimerColor;
		}
		else
		{
			soundsButton.color = Color.white;
		}
	}

	public void ButtonMusic()
	{
		bool musicEnabled = musicReclaim.ManipulateMusicValue();

		if (!musicEnabled)
		{
			mueisButton.color = reclaimerColor;
		}
		else
		{
			mueisButton.color = Color.white;
		}
	}

	public void OnClose()
	{
		gameObject.SetActive(false);
	}
}
