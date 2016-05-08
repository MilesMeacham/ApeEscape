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

	private bool attach = false;


	void Start ()
	{
		hinge = GetComponent<HingeJoint2D> ();
		baseLink = GetComponent<DistanceJoint2D> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	public void SwingAttach ()
	{
		swingBoxCollider.gameObject.SetActive (true);
	}

	public void SwingAttachCheck ()
	{
		if (swingCollider.attach == true && hinge.enabled != true) {
			hinge.enabled = true;
			baseLink.enabled = true;
			rb.velocity = new Vector2 (rb.velocity.x * 3, rb.velocity.y);
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
