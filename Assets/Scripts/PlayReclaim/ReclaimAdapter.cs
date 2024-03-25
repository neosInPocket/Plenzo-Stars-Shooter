using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReclaimAdapter : MonoBehaviour
{
	[SerializeField] private PlayerTraveler playerTraveler;

	private void Start()
	{
		playerTraveler.SetPlayer();
	}
}
