// AUTHOR: Miles
// DESCRIPTION: Limits the jump of the player. He needs to be grounded to jump
// REQUIREMENTS: Needs "Ground Check" as a child object of this game object

using UnityEngine;
using System.Collections;

public class CharacterJump : MonoBehaviour {

	private Motor motor;
	private GroundCheck groundCheck;

//	[HideInInspector]
	public bool doubleJumped;

	void Start () 
	{
		groundCheck = GetComponentInChildren<GroundCheck> ();
		motor = GetComponent<Motor> ();

	}
		

	//keyboard jumping
	public void Jump () 
	{
		if (groundCheck.grounded && !doubleJumped)
			motor.Vertical ();
/*		else if (!doubleJumped) 
		{
			motor.Vertical ();
			doubleJumped = true;
	}
*/	
	}
}
