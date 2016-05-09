using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {

	[HideInInspector]
	public HingeJoint2D hinge;
	[HideInInspector]
	public DistanceJoint2D baseLink;
	public BoxCollider2D swingBoxCollider;
	public SwingCollider swingCollider;
	private Rigidbody2D rb;
	private Motor motor;

	private bool attach = false;

	[HideInInspector]
	public float xPos = 0;



	void Start ()
	{
		hinge = GetComponent<HingeJoint2D> ();
		baseLink = GetComponent<DistanceJoint2D> ();
		rb = GetComponent<Rigidbody2D> ();
		motor = GetComponent<Motor> ();
		swingBoxCollider.gameObject.SetActive (false);
	}

	public void SwingAttach ()
	{
		swingBoxCollider.gameObject.SetActive (true);
	}

	public void SwingAttachCheck ()
	{
		if (swingCollider.attach == true && hinge.enabled != true) {
			rb.position = new Vector2 (xPos, rb.position.y);

			hinge.enabled = true;
			baseLink.enabled = true;
		
			//rb.velocity = new Vector2 (rb.velocity.x * 2, rb.velocity.y);
			motor.RawHorizontal (Mathf.Sign (rb.velocity.x) * 4);
		} else
			SwingAttach ();

	}

	public void SwingDetach ()
	{
		if (swingCollider.attach == true) {
			swingBoxCollider.gameObject.SetActive (false);
			hinge.enabled = false;
			baseLink.enabled = false;
			swingCollider.attach = false;
		}

	}

}
