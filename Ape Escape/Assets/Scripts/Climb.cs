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


	void Start ()
	{
		//baseLink = GetComponent<DistanceJoint2D> ();
		rb = GetComponent<Rigidbody2D> ();
		motor = GetComponent<Motor> ();
		swing = GetComponent<Swing> ();
		climbBoxCollider.gameObject.SetActive (true);
	}

	public void ClimbAttach ()
	{
		
	}

	public void ClimbInDirection (float direction)
	{
		if (climbCollider.attach == true) {
			swing.climbing = true;
			swing.SwingDetach ();
			rb.position = new Vector2 (xPos, rb.position.y);
			motor.Vertical (direction * climb_speed);
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
