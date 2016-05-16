// AUTHOR: Miles
// DESCRIPTION: Checks to see if the player is on the ground or not.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotationCheck : MonoBehaviour {

	private GroundCheck groundcheck;
	public float rotateDelay = 0.1f;
	public bool collided = false;
	private bool dontRotate = false;

	List<Collider2D> colliders = new List<Collider2D>();

	void Start()
	{
		groundcheck = transform.parent.gameObject.GetComponentInChildren<GroundCheck> ();
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		// Layer 8 is the ground Layer
		if (collider.gameObject.layer == 8) {
			collided = true;
			if (!colliders.Contains (collider)) {
				colliders.Add (collider);
			}
		}
	}

	IEnumerator RotateDelayCO()
	{
		dontRotate = true;

		yield return new WaitForSeconds(rotateDelay);

		dontRotate = false;

	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.layer == 8) {
			collided = true;
			if (dontRotate == false) {
				var original_euler = collider.transform.rotation.eulerAngles;
				var euler = original_euler;
				foreach (Collider2D checking_collider in colliders) {
					var checking_euler = checking_collider.transform.rotation.eulerAngles;
					if (checking_euler.z == 0) {
						euler = checking_euler;
					}
				}
				//if (euler.z != original_euler.z) {
					transform.parent.transform.rotation = Quaternion.Euler (0, 0, -euler.z);
					if (euler.z != 0) {
						StartCoroutine (RotateDelayCO ());
					}
				//}
			}
		}
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		// Layer 8 is the ground Layer
		if (collider.gameObject.layer == 8) {
			collided = false;
			if(colliders.Contains(collider))
				colliders.Remove(collider);
			if (dontRotate) {
				StartCoroutine (ExitDelayCO ());
			} else {
				transform.parent.transform.rotation = Quaternion.Euler (0, 0, 0);
			}
		}
	}

	IEnumerator ExitDelayCO()
	{
		while (dontRotate == false) {
			yield return null;
		}
		transform.parent.transform.rotation = Quaternion.Euler (0, 0, 0);
	}
}
