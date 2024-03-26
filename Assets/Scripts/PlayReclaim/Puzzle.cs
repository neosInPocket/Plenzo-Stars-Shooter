using UnityEngine;

public class Puzzle : MonoBehaviour
{
	[SerializeField] private new Rigidbody rigidbody;

	public void StartRoute(float velocity)
	{
		rigidbody.velocity = Vector3.left * velocity;
	}

	private void Update()
	{
		if (transform.position.x < -40f)
		{
			Destroy(gameObject);
		}
	}
}
