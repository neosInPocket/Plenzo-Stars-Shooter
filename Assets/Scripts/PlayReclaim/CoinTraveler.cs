using System;
using UnityEngine;

public class CoinTraveler : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private GameObject collectParticles;
	[SerializeField] private Collider coinCollider;
	[SerializeField] private GameObject chargeParticles;
	public Action<CoinTraveler> CoinCollected { get; set; }

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.name == "PlayerTraveller")
		{
			CoinCollected?.Invoke(this);
			collectParticles.SetActive(true);
			meshRenderer.enabled = false;
			coinCollider.enabled = false;
			chargeParticles.SetActive(false);
		}
	}
}
