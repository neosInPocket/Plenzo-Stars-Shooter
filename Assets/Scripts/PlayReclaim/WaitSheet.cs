using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using System;

public class WaitSheet : MonoBehaviour
{
	public Action Awaited { get; set; }
	public void WaitForInput()
	{
		gameObject.SetActive(true);

		Touch.onFingerDown += TouchHandler;
	}

	private void TouchHandler(Finger figner)
	{
		Touch.onFingerDown -= TouchHandler;
		Awaited?.Invoke();
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= TouchHandler;
	}
}
