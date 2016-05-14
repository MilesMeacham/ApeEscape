// AUTHOR: Miles
// DESCRIPTION: Checks to see if the player is on the ground or not.

using UnityEngine;
using System.Collections;

public class BlockedCheck : MonoBehaviour {

	public bool blocked;

	void OnTriggerEnter2D (Collider2D collider)
	{
		// Layer 8 is the ground Layer
		if (collider.gameObject.layer == 8) {
			blocked = true;
		}
	}


	// MIGHT NOT NEED THIS
	void OnTriggerStay2D (Collider2D collider)
	{
		// Layer 8 is the ground Layer
		if (collider.gameObject.layer == 8) 
			blocked = true;
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		// Layer 8 is the ground Layer
		if (collider.gameObject.layer == 8) 
			blocked = false;
	}
}
