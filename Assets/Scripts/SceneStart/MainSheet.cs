using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSheet : MonoBehaviour
{
	[SerializeField] private GameObject shopSheet;

	public void OnShopTransition()
	{
		shopSheet.gameObject.SetActive(true);
	}

	public void OnPlayTransition()
	{
		SceneManager.LoadScene("PlayReclaim");
	}
}
