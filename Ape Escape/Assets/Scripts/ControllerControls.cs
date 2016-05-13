using UnityEngine;
using System.Collections;

public class ControllerControls : MonoBehaviour {

	private Motor motor;
	private CharacterJump jump;
	private Swing swing;
	private bool attachStart;

	void Start () 
	{
		motor = GetComponent<Motor> ();
		jump = GetComponent<CharacterJump> ();
		swing = GetComponent<Swing> ();
	}
	

	void Update () 
	{
		if (Input.GetButtonDown ("Jump"))
			jump.Jump ();



		// SWING STUFF    RT is negative 3rd Axis
		if (Input.GetAxis ("Swing") < 0 && !attachStart) 
		{
			swing.SwingAttach ();

			attachStart = true;
		}

		if (Input.GetAxis ("Swing") < 0)
			swing.SwingAttachCheck ();

		if (Input.GetAxis ("Swing") >= 0 && !Input.GetKey (KeyCode.M)) 
		{
			swing.SwingDetach ();
			attachStart = false;
		}
			
	}

	void FixedUpdate ()
	{
		motor.Horizontal (Input.GetAxis ("Horizontal"));


	}
}
