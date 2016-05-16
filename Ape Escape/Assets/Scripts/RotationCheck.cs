// AUTHOR: Miles
// DESCRIPTION: Checks to see if the player is on the ground or not.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotationCheck : MonoBehaviour {

	private GroundCheck groundcheck;
	public float rotateDelay = 0.3f;
	public bool collided = false;
	private bool dontRotate = false;

	[HideInInspector]
	public List<Collider2D> colliders = new List<Collider2D>();

	void Start()
	{
		groundcheck = transform.parent.gameObject.GetComponentInChildren<GroundCheck> ();
		colliders = new List<Collider2D>();
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
				var original_euler = transform.parent.transform.rotation.eulerAngles;
				var new_euler = collider.transform.rotation.eulerAngles;
				var euler = new_euler;
				foreach (Collider2D checking_collider in colliders) {
					var checking_euler = checking_collider.transform.rotation.eulerAngles;
					if (checking_euler.z == 0) {
						euler = checking_euler;
					}
				}
				if (original_euler.z != euler.z && euler.z != 0) {
					StartCoroutine (RotateDelayCO ());
					transform.parent.transform.rotation = Quaternion.Euler (0, 0, -euler.z);
				}
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

	public void Reset()
	{
		colliders = new List<Collider2D>();
		dontRotate = false;
	}
}
