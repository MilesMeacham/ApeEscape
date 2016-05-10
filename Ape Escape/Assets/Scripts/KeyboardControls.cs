// AUTHOR: Miles
// DESCRIPTION: Handles the keyboard controls

using UnityEngine;
using System.Collections;

public class KeyboardControls : MonoBehaviour {

	private Motor motor;
	private CharacterJump jump;
	private Swing swing;
	private float stop = 0;


	void Start () 
	{
		motor = GetComponent<Motor> ();
		jump = GetComponent<CharacterJump> ();
		swing = GetComponent<Swing> ();
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
		if (Input.GetKeyDown (KeyCode.Space))
			jump.Jump ();

		// SWING STUFF
		if (Input.GetKeyDown (KeyCode.F))
			swing.SwingAttach ();

		if (Input.GetKey (KeyCode.F))
			swing.SwingAttachCheck ();

		if (Input.GetKeyUp (KeyCode.F))
			swing.SwingDetach ();


	}

	// Use for Movement (This avoids moving faster or slower due to framerate)
	void FixedUpdate()
	{
		// RIGHT
		if (Input.GetKey (KeyCode.D))
			motor.Horizontal (1);

		// LEFT
		if (Input.GetKey (KeyCode.A))
			motor.Horizontal (-1);



	}
}
