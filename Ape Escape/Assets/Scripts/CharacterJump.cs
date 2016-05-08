// AUTHOR: Miles
// DESCRIPTION: Limits the jump of the player. He needs to be grounded to jump
// REQUIREMENTS: Needs "Ground Check" as a child object of this game object

using UnityEngine;
using System.Collections;

public class CharacterJump : MonoBehaviour {

	private Motor motor;
	private GroundCheck groundCheck;
	private SwingCollider swingCollider;
	private Swing swing;


	void Start () 
	{
		groundCheck = GetComponentInChildren<GroundCheck> ();
		motor = GetComponent<Motor> ();
		swingCollider = GetComponentInChildren<SwingCollider> ();
		swing = GetComponentInChildren<Swing> ();
	}
		

	//keyboard jumping
	public void Jump () 
	{
		if (groundCheck.grounded)
			motor.Vertical ();
		if (swingCollider.attach) {
			swing.SwingDetach ();
			motor.Vertical ();
		}
	}
}
