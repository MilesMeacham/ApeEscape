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

	private bool attach = false;

	[HideInInspector]
	public float xPos = 0;


	private float swingDelay = 0.5f;
	private bool swingDetachDelay = false;


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
        swingBoxCollider.gameObject.SetActive(true);
	}

	public void SwingAttachCheck ()
	{
		if (swingBoxCollider.gameObject.active == false && (swingDetachDelay == false || last_attached_id != current_rope_id))
			swingBoxCollider.gameObject.SetActive(true);

        if (swingCollider.attach == true && hinge.enabled != true) 
        {
            rb.position = new Vector2 (xPos, rb.position.y);

			hinge.enabled = true;
			baseLink.enabled = true;
		
			motor.RawHorizontal ((Mathf.Sign (rb.velocity.x) * Mathf.Abs(rb.velocity.y)) * 2);
		} 

	}

	public void SwingDetach ()
	{
            if (hinge.enabled)
            {
                last_attached_id = current_rope_id;
            }
			swingBoxCollider.gameObject.SetActive (false);
			hinge.enabled = false;
			baseLink.enabled = false;
			swingCollider.attach = false;
	

		if (!swingDetachDelay)
			StartCoroutine(SwingDetachDelayCO());

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
			current_rope_id = collider.transform.root.GetInstanceID();
			connected_hinge = collider.transform.root.GetComponentInChildren<HingeJoint2D> ();
		}
	}


    void FixedUpdate()
    {
		
    }

}
