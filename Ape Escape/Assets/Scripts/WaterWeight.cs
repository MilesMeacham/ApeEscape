using UnityEngine;
using System.Collections;

public class WaterWeight : MonoBehaviour {

	// This weight makes it so the box sits flat, not at an angle
	public GameObject waterWeight;

	void OnTriggerEnter2D (Collider2D collider)
	{

		if (collider.gameObject.tag == "Water")
			waterWeight.SetActive (true);

	}

	void OnTriggerExit2D (Collider2D collider)
	{

		if (collider.gameObject.tag == "Water")
			waterWeight.SetActive (true);

	}
}
