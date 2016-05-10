using UnityEngine;
using System.Collections;

public class SwingCollider : MonoBehaviour {

	public bool attach = false;
	public Swing swing;

    void OnTriggerEnter2D(Collider2D collider)
    {
        OnTriggerStay2D(collider);
    }

	void OnTriggerStay2D (Collider2D collider)
	{
        swing.current_rope_id = collider.transform.root.GetInstanceID();
        if (collider.gameObject.layer == 9 && attach == false) 
		{
			swing.hinge.connectedBody = collider.gameObject.GetComponent<Rigidbody2D> ();
			swing.baseLink.connectedBody = collider.transform.parent.GetComponent<Rigidbody2D> ();
			swing.xPos = collider.gameObject.GetComponent<Rigidbody2D> ().position.x;

            attach = true;
		}
    }

	void OnTriggerExit2D (Collider2D collider)
	{
		if (collider.gameObject.layer == 9 && attach == false) 
		{
			swing.xPos = 0;
			attach = false;
            swing.current_rope_id = 0;

        }
	}

}
