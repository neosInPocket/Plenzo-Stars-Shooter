using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerTraveler : MonoBehaviour
{
	[SerializeField] private MeshRenderer mesh;
	[SerializeField] private GameObject playerTrail;
	[SerializeField] new private Rigidbody rigidbody;
	[SerializeField] private Vector3 startPosition;
	[SerializeField] private float shootMagnitude;
	[SerializeField] private Vector2 shootSpeeds;
	[SerializeField] private Reclaimer reclaimer;
	[SerializeField] private float currentDeltaThreshold;
	[SerializeField] private GameObject slippedEffect;
	[SerializeField] private AudioSource coinSource;
	[SerializeField] private AudioSource explosionSource;
	private float shootSpeed;
	private Vector3 currentVelocityDirection;
	private bool slipped;
	private bool destroyed;
	public Action Slipped { get; set; }

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		explosionSource.enabled = reclaimer.reclaimData.reclaimSoundEffect;
		coinSource.enabled = reclaimer.reclaimData.reclaimSoundEffect;

		if (reclaimer.reclaimData.reclaimEffect1)
		{
			shootSpeed = shootSpeeds.y;
		}
		else
		{
			shootSpeed = shootSpeeds.x;
		}

		startPos = default;
		currentVelocityDirection = default;
	}

	private void Update()
	{
		if (transform.position.y < startPosition.y - 1)
		{
			Slipped?.Invoke();
		}

		if (slipped || destroyed) return;
		rigidbody.velocity = currentVelocityDirection * shootSpeed;
	}

	public void SetPlayer()
	{
		Touch.onFingerDown += OnPlayerDown;
		Touch.onFingerUp += OnPlayerUp;
	}

	private Vector2 startPos;
	private Vector2 currentDelta;
	private Vector2 screenSize => new Vector2(Screen.width, Screen.height);
	private bool fingerDowned;

	private void OnPlayerDown(Finger finger)
	{
		startPos = finger.screenPosition;
		fingerDowned = true;
	}

	private void OnPlayerUp(Finger finger)
	{
		if (!fingerDowned) return;
		fingerDowned = false;

		currentDelta = (finger.screenPosition - startPos).normalized;
		if (currentDelta.magnitude < currentDeltaThreshold) return;

		if (Mathf.Abs(currentDelta.x) > Mathf.Abs(currentDelta.y))
		{
			int direction = (int)(currentDelta.x / Mathf.Abs(currentDelta.x));
			currentVelocityDirection = new Vector3(0, 0, -direction);
		}
		else
		{
			int direction = (int)(currentDelta.y / Mathf.Abs(currentDelta.y));
			currentVelocityDirection = new Vector3(direction, 0, 0);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.transform.parent.GetComponent<Puzzle>() != null)
		{
			PlayerDead();
			slipped = true;
			destroyed = true;
			Slipped?.Invoke();
			return;
		}
	}

	public void PlayerDead()
	{
		destroyed = true;
		mesh.enabled = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		rigidbody.velocity = Vector3.zero;
		slippedEffect.gameObject.SetActive(true);
		Touch.onFingerDown -= OnPlayerDown;
		Touch.onFingerUp -= OnPlayerUp;
	}

	public void PlayerWin()
	{
		destroyed = true;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		rigidbody.velocity = Vector3.zero;
		Touch.onFingerDown -= OnPlayerDown;
		Touch.onFingerUp -= OnPlayerUp;
	}

	private void OnCollisionStay(Collision collision)
	{
		slipped = false;
	}

	private void OnCollisionExit(Collision collision)
	{
		//if (collision.gameObject.name == "Connector") return;
		slipped = true;
		rigidbody.constraints = RigidbodyConstraints.None;
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnPlayerDown;
		Touch.onFingerUp -= OnPlayerUp;
	}

	public Action CoinHandler { get; set; }

	public void CoinCallback()
	{
		coinSource.Stop();
		coinSource.Play();
		CoinHandler?.Invoke();
	}
}
