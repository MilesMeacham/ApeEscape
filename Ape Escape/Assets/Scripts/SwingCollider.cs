using UnityEngine;
using System.Collections;

public class SwingCollider : MonoBehaviour {

	public bool attach = false;
	public Swing swing;

	void OnTriggerStay2D (Collider2D collider)
	{
		if (collider.gameObject.layer == 9 && attach == false) 
		{
			
			swing.hinge.connectedBody = collider.gameObject.GetComponent<Rigidbody2D> ();
			swing.baseLink.connectedBody = collider.transform.parent.GetComponent<Rigidbody2D> ();
			attach = true;
		}
	}

}
