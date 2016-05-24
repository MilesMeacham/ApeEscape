using UnityEngine;
using System.Collections;

public class Climb : MonoBehaviour {

	//[HideInInspector]
	//public DistanceJoint2D baseLink;
	public BoxCollider2D climbBoxCollider;
	public ClimbCollider climbCollider;
	private Rigidbody2D rb;
	//private Motor motor;
	private Swing swing;
	public int current_rope_id;
	public float climb_speed = 5f;

	[HideInInspector]
	public float xPos = 0;

	[HideInInspector]
	public bool climbing = false;

	private Transform up_position;


	void Start ()
	{
		//baseLink = GetComponent<DistanceJoint2D> ();
		rb = GetComponent<Rigidbody2D> ();
		//motor = GetComponent<Motor> ();
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
			rb.position = new Vector2 (xPos, rb.position.y);
			transform.Translate(Vector3.up * Time.deltaTime * climb_speed * direction, up_position);
		} else {
			swing.climbing = false;
		}

	}

	public void ClimbDetach ()
	{
		if (climbCollider.attach) {
			climbCollider.attach = false;
			climbing = false;
			swing.climbing = false;
			//swing.baseLink.enabled = true;
		}

	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.layer == 9){
			current_rope_id = collider.transform.parent.parent.GetInstanceID();
			if (climbing) {
				//swing.baseLink.enabled = false;
				swing.hinge.connectedBody = collider.transform.GetComponent<Rigidbody2D> ();
			}
			up_position = transform.GetComponent<HingeJoint2D> ().connectedBody.transform;

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
