// AUTHOR: Miles
// DESCRIPTION: Checks to see if the player is on the ground or not.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotationCheck : MonoBehaviour {

	private GroundCheck groundcheck;
	public float rotateSpeed = 1f;
	public bool collided = false;
	private bool doneRotating = true;

	private float target_angle = 0f;


	void Start()
	{
		groundcheck = transform.parent.gameObject.GetComponentInChildren<GroundCheck> ();
	}

	float reverseAngle(float angle)
	{
		float new_angle = 360 - angle;
		if (new_angle == 360) {
			new_angle = 0;
		}

		return new_angle;
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		// Layer 8 is the ground Layer
		if (collider.gameObject.layer == 8) {
			collided = true;
			OnTriggerStay2D (collider);
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.layer == 8) {
			collided = true;
			var original_euler = transform.parent.transform.rotation.eulerAngles;
			var new_euler = collider.transform.rotation.eulerAngles;

			if (original_euler.z != new_euler.z) {
				target_angle = new_euler.z;
			}
		}
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		// Layer 8 is the ground Layer
		if (collider.gameObject.layer == 8) {
			collided = false;
			if(!groundcheck.grounded && !collided) {
				target_angle = 0;
			}
		}
	}

	void FixedUpdate()
	{
		var original_angle = transform.parent.transform.rotation.eulerAngles.z;
		//string printed = "original_angle: " + original_angle + " target_angle: " + target_angle;
		//print (printed);
		if (original_angle > target_angle) {
			if (Mathf.Abs(original_angle - target_angle) < rotateSpeed) {
				transform.parent.transform.rotation = Quaternion.Euler (0, 0, target_angle);
				doneRotating = true;
			} else if (Mathf.Abs (original_angle - target_angle) > 180) {
				transform.parent.transform.rotation = Quaternion.Euler (0, 0, original_angle + rotateSpeed);
				doneRotating = false;
			} else {
				transform.parent.transform.rotation = Quaternion.Euler (0, 0, original_angle - rotateSpeed);
				doneRotating = false;
			}
		} else if (original_angle < target_angle) {
			if (Mathf.Abs(target_angle - original_angle) < rotateSpeed) {
				transform.parent.transform.rotation = Quaternion.Euler (0, 0, target_angle);
				doneRotating = true;
			} else if (Mathf.Abs (target_angle - original_angle) > 180) {
				transform.parent.transform.rotation = Quaternion.Euler (0, 0, original_angle - rotateSpeed);
				doneRotating = false;
			} else {
				transform.parent.transform.rotation = Quaternion.Euler (0, 0, original_angle + rotateSpeed);
				doneRotating = false;
			}
		} else {
			doneRotating = true;
		}
	}

	public void Reset()
	{
		collided = false;
		target_angle = 0;
		doneRotating = true;
	}
}
