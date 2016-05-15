using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


	private GameObject target;  // Will usually be the player
	private Rigidbody2D targetRB;
	private float targetVelocity;
	private Rigidbody2D cameraRB;
	private Vector3 lastPosition;
	private float distance;
	private float speed = 2.5f;
	private float distanceAhead = 2;

	public float normalSpeed = 2.5f;
	public float catchUpSpeed = 2f;   // This is the speed that is added on to the players velocity
	public float lowestPoint = -10;
	public float furthestPoint = 10;


	// Use this for initialization
	void Start () 
	{
		target = GameObject.Find ("Player");
		targetRB = target.GetComponent<Rigidbody2D> ();

		cameraRB = GetComponent<Rigidbody2D> ();

		lastPosition = target.transform.position;
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		CameraMovement ();
	}

	void CameraMovement()
	{

		distance = transform.position.x - lastPosition.x;
		targetVelocity = targetRB.velocity.x;

		transform.position = Vector3.Lerp (transform.position, target.transform.position, speed * Time.deltaTime);

		if (distance >= furthestPoint && speed == normalSpeed)
			speed = targetRB.velocity.x + catchUpSpeed;
		else if (distance < furthestPoint - 1 && speed != normalSpeed)
			speed = normalSpeed;


	}
}




// Look ahead stuff
/*

// Adjust the camera so you can see more in front of you
if (targetVelocity <= 0) 
{
	if (distance >= -distanceAhead)
		speed = -8;
	else
		speed = targetVelocity;
}
else if (targetVelocity > 0) 
{
	if (distance <= distanceAhead)
		speed = 8;
	else
		speed = targetVelocity;
}


// Keeps camera from going below a certain point
if (transform.position.y < lowestPoint)
	transform.position = new Vector2 (transform.position.x, lowestPoint);



cameraRB.velocity = new Vector2 (speed, cameraRB.velocity.y);

lastPosition = target.transform.position;
*/