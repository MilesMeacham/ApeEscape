// AUTHOR: Miles
// DESCRIPTION: This handles the movement of the characters (Player and Enemies)

using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

	private Rigidbody2D rb;
	private GroundCheck groundcheck;

	public float jumpForce = 7;
	public float groundedAcceleration = 1f;
	public float airborneAcceleration = 0.5f;
	public float maxRunSpeed = 9f;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		groundcheck = GetComponentInChildren<GroundCheck> ();
	}


	// Pass a number between 1 and -1 for right and left and 0 to change nothing
	public void Horizontal(float direction)
	{
		float velocityDelta;
		if(groundcheck.grounded)
			velocityDelta = direction * groundedAcceleration;
		else
			velocityDelta = direction * airborneAcceleration;
		

		if ((direction < 0 && rb.velocity.x > -maxRunSpeed) || (direction > 0 && rb.velocity.x < maxRunSpeed))
		{
			float newVelocity = rb.velocity.x + velocityDelta;
			if (newVelocity > maxRunSpeed)
				newVelocity = maxRunSpeed;
			else if (newVelocity < -maxRunSpeed)
				newVelocity = -maxRunSpeed;
			rb.velocity = new Vector2 (newVelocity, rb.velocity.y);
		}

		


		print (rb.velocity);
	}

	// This makes the player jump.
	public void Vertical()
	{

		// WE NEED TO DECIDE BETWEEN THESE TWO WAYS OF JUMPING

		rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
		//rb.AddForce (new Vector2 (rb.velocity.x, jumpForce));
	}





}