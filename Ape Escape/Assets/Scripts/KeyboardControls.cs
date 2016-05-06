// AUTHOR: Miles
// DESCRIPTION: Handles the keyboard controls

using UnityEngine;
using System.Collections;

public class KeyboardControls : MonoBehaviour {

	private Motor motor;
	private CharacterJump jump;
	private float stop = 0;


	void Start () 
	{
		motor = GetComponent<Motor> ();
		jump = GetComponent<CharacterJump> ();
	}

	// Called Every Frame. Use for Jumping and Shooting (Not Movement)
	void Update () 
	{
		// LEFT STOP
//		if (Input.GetKeyUp (KeyCode.A))
//			motor.Horizontal (stop);

		// RIGHT STOP
//		if (Input.GetKeyUp (KeyCode.D))
//			motor.Horizontal (stop);

		// JUMP
		if (Input.GetKey (KeyCode.Space))
			jump.Jump ();


	}

	// Use for Movement (This avoids moving faster or slower due to framerate)
	void FixedUpdate()
	{
		// RIGHT
		if (Input.GetKey (KeyCode.D))
			motor.RightHorizontal (1);

		// LEFT
		if (Input.GetKey (KeyCode.A))
			motor.LeftHorizontal (-1);



	}
}
