// AUTHOR: Miles and Allen
// DESCRIPTION: This handles the movement of the characters (Player and Enemies)

using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {
	[HideInInspector]
	public Rigidbody2D rb;
	private GroundCheck groundcheck;
	private SwingCollider swingCollider;
	//private float direction = 1;
	private Vector3 scale;

	public float jumpForce = 9;
	public float swingJumpForce = 7;
	public float groundedAcceleration = 1f;
	public float airborneAcceleration = 0.5f;
	public float swingingAcceleration = 9f;
	public float maxRunSpeed = 9f;
	public float maxSwingSpeed = 9f;

	public bool facingRight = true;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		groundcheck = GetComponentInChildren<GroundCheck> ();
		swingCollider = GetComponentInChildren<SwingCollider> ();
		scale = transform.localScale;
	}



	public void Horizontal(float direction)
	{
		if (!swingCollider.attach) {
			if (direction > 0 && !facingRight)
				Flip ();
			else if (direction < 0 && facingRight)
				Flip ();
		}

		float maxSpeedAdjusted;
		float velocityDelta;

		if (swingCollider.attach) 
		{
			velocityDelta = direction * swingingAcceleration;
			maxSpeedAdjusted = maxSwingSpeed * direction;
		}
		else if (groundcheck.grounded) {
			maxSpeedAdjusted = maxRunSpeed * direction;
			velocityDelta = direction * groundedAcceleration;
		} else {
			maxSpeedAdjusted = maxRunSpeed * direction;
			velocityDelta = direction * airborneAcceleration;
		}
		
		float newVelocity = rb.velocity.x + velocityDelta;

		if (direction < 0 && rb.velocity.x > maxSpeedAdjusted) 
		{
			if (newVelocity < maxSpeedAdjusted)
				newVelocity = maxSpeedAdjusted;
			
			rb.velocity = new Vector2 (newVelocity, rb.velocity.y);
		} 
		else if (direction > 0 && rb.velocity.x < maxSpeedAdjusted) 
		{
			if (newVelocity > maxSpeedAdjusted)
				newVelocity = maxSpeedAdjusted;
			
			rb.velocity = new Vector2 (newVelocity, rb.velocity.y);
		}
			
	}

	public void RawHorizontal(float direction)
	{
		float newVelocity = rb.velocity.x + direction;
		rb.velocity = new Vector2 (newVelocity, rb.velocity.y);
	}

	// This makes the player jump.
	public void Vertical(float force = 0f)
	{
		if (force == 0f) {
			force = jumpForce;
		}

		// WE NEED TO DECIDE BETWEEN THESE TWO WAYS OF JUMPING
		rb.velocity = new Vector2 (rb.velocity.x, force);
		//rb.AddForce (new Vector2 (rb.velocity.x, jumpForce));
	}

	void Flip()
	{
		facingRight = !facingRight;

		// Reverses the speed;
		//direction *= -1;

		scale.x *= -1;
		transform.localScale = scale;

	}





}