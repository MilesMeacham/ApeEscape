// AUTHOR: Miles and Allen
// DESCRIPTION: This handles the movement of the characters (Player and Enemies)

using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {
	[HideInInspector]
	public Rigidbody2D rb;
	private GroundCheck groundcheck;
	private BlockedCheck blockedcheck;
	private SwingCollider swingCollider;
	//private float direction = 1;
	private Vector3 scale;
	private Animator animator;

	private float swingingAccelerationModified = 0;

	public float jumpForce = 9;
	public float swingJumpForce = 7;
	public float groundedAcceleration = 1f;
	public float airborneAcceleration = 0.5f;
	public float swingingAcceleration = 0.002f;
	public float maxRunSpeed = 9f;
	public float maxSwingSpeed = 15f;

	public bool facingRight = true;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		groundcheck = GetComponentInChildren<GroundCheck> ();
		blockedcheck = GetComponentInChildren<BlockedCheck> ();
		swingCollider = GetComponentInChildren<SwingCollider> ();
		animator = GetComponentInChildren<Animator> ();

		scale = transform.localScale;
		swingingAccelerationModified = swingingAcceleration;

	}


	void Update ()
	{
		// Just sending the absolute value of velocity for now.
		if(groundcheck.grounded)
			animator.SetFloat ("speed", Mathf.Abs (rb.velocity.x));
		else
			animator.SetFloat ("speed", 0);

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
		float vertical_accel = 0f;

		if (swingCollider.attach) 
		{
			float swingingAccelerationModified = (Mathf.Abs (rb.velocity.x) + Mathf.Abs (rb.velocity.y)) * swingingAcceleration;
			if (swingingAccelerationModified < swingingAcceleration)
				swingingAccelerationModified = swingingAcceleration;
			float average_angle = (swingCollider.swing.hinge.jointAngle + -swingCollider.swing.connected_hinge.jointAngle) / 2;
			float accel = (swingingAccelerationModified);// - (Mathf.Abs(average_angle) * 0.08f);
			if (Mathf.Sign (rb.velocity.x) != Mathf.Sign (direction)  || Mathf.Abs(average_angle) >= 90) {
				accel = 0;//accel - (Mathf.Abs(average_angle) * 2);
			} else
				accel += 1;
			if (accel < 0)
				accel = 0;

			float angle_ratio = 90 / Mathf.Abs (average_angle);
			vertical_accel = accel / angle_ratio;
			accel = accel - vertical_accel;
			if (rb.velocity.y < 0)
				vertical_accel = -vertical_accel;

			velocityDelta = direction * accel;
			maxSpeedAdjusted = maxSwingSpeed * direction;
			swingingAccelerationModified = swingingAcceleration;
		}
		else if (groundcheck.grounded) {
			maxSpeedAdjusted = maxRunSpeed * direction;
			velocityDelta = direction * groundedAcceleration;
		} else {
			if (!blockedcheck.blocked) {
				maxSpeedAdjusted = maxRunSpeed * direction;
				velocityDelta = direction * airborneAcceleration;
			} else {
				maxSpeedAdjusted = 0;
				velocityDelta = 0;
			}
				
		}
		
		float newVelocity = rb.velocity.x + velocityDelta;
		float y_velocity = rb.velocity.y + vertical_accel;

		if (direction < 0 && rb.velocity.x > maxSpeedAdjusted) 
		{
			if (newVelocity < maxSpeedAdjusted)
				newVelocity = maxSpeedAdjusted;
			
			rb.velocity = new Vector2 (newVelocity, y_velocity);
		} 
		else if (direction > 0 && rb.velocity.x < maxSpeedAdjusted) 
		{
			if (newVelocity > maxSpeedAdjusted)
				newVelocity = maxSpeedAdjusted;
			
			rb.velocity = new Vector2 (newVelocity, y_velocity);
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