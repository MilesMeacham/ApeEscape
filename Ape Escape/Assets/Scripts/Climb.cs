using UnityEngine;
using System.Collections;

public class Climb : MonoBehaviour {

	//[HideInInspector]
	//public DistanceJoint2D baseLink;
	public BoxCollider2D climbBoxCollider;
	public ClimbCollider climbCollider;
	private Rigidbody2D rb;
	private Motor motor;
	private Swing swing;
	public int current_rope_id;
	public float climb_speed = 3f;

	[HideInInspector]
	public float xPos = 0;

	[HideInInspector]
	public bool climbing = false;

	private Vector3 up_position;


	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		motor = GetComponent<Motor> ();
		swing = GetComponent<Swing> ();
		climbBoxCollider.gameObject.SetActive (true);
	}

	public void ClimbAttach ()
	{
		
	}

	public void ClimbUp ()
	{
		if (climbCollider.attach == true) {
			swing.climbing = true;
			swing.SwingDetach ();
			float y_magnitude = rb.position.y - up_position.y;
			y_magnitude = -y_magnitude;

			float x_magnitude = rb.position.x - up_position.x;
			x_magnitude = -x_magnitude;

			rb.position = new Vector2 (rb.position.x + x_magnitude, rb.position.y + y_magnitude);
			//motor.Vertical (y_magnitude * climb_speed);
			//motor.RawHorizontal(x_magnitude * climb_speed);
		} else {
			swing.climbing = false;
		}

	}

	public void ClimbDetach ()
	{
		if (climbCollider.attach) {
			//baseLink.enabled = false;
			climbCollider.attach = false;
			climbing = false;
			swing.climbing = false;
		}

	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.layer == 9){
			current_rope_id = collider.transform.parent.parent.GetInstanceID();
			up_position = transform.GetComponent<HingeJoint2D>().connectedBody.GetComponent<HingeJoint2D>().connectedBody.transform.position;
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		//if(collider.gameObject.layer == 9){
		//	current_rope_id = collider.transform.root.GetInstanceID();
		//}
	}


	void FixedUpdate()
	{

	}

}
