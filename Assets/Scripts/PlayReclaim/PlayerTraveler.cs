using System;
using UnityEditor.Hardware;
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
	private float shootSpeed;
	private Vector3 currentVelocityDirection;
	private bool slipped;
	public Action Slipped { get; set; }

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
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

		if (slipped) return;
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

	private void OnPlayerDown(Finger finger)
	{
		startPos = finger.screenPosition;
	}

	private void OnPlayerUp(Finger finger)
	{
		currentDelta = (finger.screenPosition - startPos).normalized;

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

	private void OnTriggerEnter(Collider collider)
	{

	}

	private void OnCollisionEnter(Collision collision)
	{
		slipped = false;
	}

	public void PlayerDead()
	{
		mesh.enabled = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.name == "Connector") return;
		slipped = true;
		rigidbody.constraints = RigidbodyConstraints.None;
		Debug.Log("exit");
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnPlayerDown;
		Touch.onFingerUp -= OnPlayerUp;
	}
}
