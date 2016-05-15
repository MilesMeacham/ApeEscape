using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {

	[HideInInspector]
	public HingeJoint2D hinge;
	[HideInInspector]
	public HingeJoint2D connected_hinge;
	[HideInInspector]
	public DistanceJoint2D baseLink;
	public BoxCollider2D swingBoxCollider;
	public SwingCollider swingCollider;
    public int last_attached_id;
	private Rigidbody2D rb;
	private Motor motor;
    public int current_rope_id;
	public float boostMultiplier = 1.5f;

	[HideInInspector]
	public float xPos = 0;

	private float swingDelay = 0.5f;
	private bool swingDetachDelay = false;
	private bool climbing = false;


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
		if (swingBoxCollider.gameObject.activeSelf == false && (swingDetachDelay == false || last_attached_id != current_rope_id))
			swingBoxCollider.gameObject.SetActive (true);
	}

	public void SwingAttachCheck ()
	{
		if (swingBoxCollider.gameObject.activeSelf == false && (swingDetachDelay == false || climbing || last_attached_id != current_rope_id))
			swingBoxCollider.gameObject.SetActive(true);

        if (swingCollider.attach == true && hinge.enabled != true) 
        {
            rb.position = new Vector2 (xPos, rb.position.y);

			hinge.enabled = true;
			baseLink.enabled = true;
		
			if (!climbing) {
				float boost = rb.velocity.x;
				if (rb.velocity.y < 0)
					boost += (Mathf.Abs (rb.velocity.y) * Mathf.Sign (rb.velocity.x));
				if (Mathf.Abs (boost) < 2)
					boost = Mathf.Sign (boost) * 2;
				boost *= boostMultiplier;
				motor.RawHorizontal (boost);
			} 
		} 

	}

	public void SwingDetach ()
	{
		if (swingCollider.attach && !swingDetachDelay)
			StartCoroutine (SwingDetachDelayCO ());
        if (hinge.enabled)
            last_attached_id = current_rope_id;
		if (swingCollider.attach) {
			swingBoxCollider.gameObject.SetActive (false);
			hinge.enabled = false;
			baseLink.enabled = false;
			swingCollider.attach = false;
		}

	}


	IEnumerator SwingDetachDelayCO()
	{
		swingDetachDelay = true;

		yield return new WaitForSeconds(swingDelay);

		swingDetachDelay = false;

	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.layer == 9){
			current_rope_id = collider.transform.parent.parent.GetInstanceID();
			connected_hinge = collider.transform.root.GetComponentInChildren<HingeJoint2D> ();
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		//if(collider.gameObject.layer == 9){
		//	current_rope_id = collider.transform.root.GetInstanceID();
		//}
	}

	public void climb(float new_position = 0f)
	{
		if(swingCollider.attach){
			SwingDetach();
			rb.position = new Vector2 (rb.position.x, rb.position.y + new_position);
			climbing = true;
		}
	}

	public void stopClimb()
	{
		climbing = false;
	}


    void FixedUpdate()
    {
		
    }

}
