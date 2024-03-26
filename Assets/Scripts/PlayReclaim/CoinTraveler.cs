using System;
using UnityEngine;

public class CoinTraveler : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private GameObject collectParticles;
	[SerializeField] private Collider coinCollider;
	[SerializeField] private GameObject chargeParticles;
	[SerializeField] private float rotateSpeed;
	public Action<CoinTraveler> CoinCollected { get; set; }
	private Vector2 currentRotation;

	private void Start()
	{
		currentRotation = transform.eulerAngles;
	}

	private void Update()
	{
		currentRotation.y += rotateSpeed * Time.deltaTime;
		transform.eulerAngles = currentRotation;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.name == "PlayerTraveller")
		{
			var player = collider.GetComponent<PlayerTraveler>();
			player.CoinCallback();

			CoinCollected?.Invoke(this);
			collectParticles.SetActive(true);
			meshRenderer.enabled = false;
			coinCollider.enabled = false;
			chargeParticles.SetActive(false);
		}
	}
}
