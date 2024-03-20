using UnityEngine;

public class ShopSheet : MonoBehaviour
{
	[SerializeField] private Animator mainSheet;

	public void OnTransitionEnd()
	{
		mainSheet.SetTrigger("begin");
		gameObject.SetActive(false);
	}
}
