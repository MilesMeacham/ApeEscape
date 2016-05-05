// AUTHOR: Miles
// DESCRIPTION: This handles the movement of the characters (Player and Enemies)

using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

	private Rigidbody2D rb;

	public float jumpForce = 7;
	public float groundedAcceleration = 7f;
	public float airborneAcceleration = 0.1f;
	public float maxRunSpeed = 7;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
	}


	// Pass a number between 1 and -1 for right and left and 0 to change nothing
	public void Horizontal(float direction)
	{
		float velocityDelta = direction * groundedAcceleration;

		if ((direction < 0 && rb.velocity.x > -maxRunSpeed) || (direction > 0 && rb.velocity.x < maxRunSpeed))
			rb.velocity = new Vector2 (rb.velocity.x + velocityDelta, rb.velocity.y);

		


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