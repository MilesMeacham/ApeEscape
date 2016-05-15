using UnityEngine;
using System.Collections;

public class ClimbCollider : MonoBehaviour {

	public bool attach = false;
	public Climb climb;

	void OnTriggerEnter2D(Collider2D collider)
	{
		OnTriggerStay2D(collider);
	}

	void OnTriggerStay2D (Collider2D collider)
	{
		if (collider.gameObject.layer == 9 && attach == false) 
		{
			climb.xPos = collider.gameObject.GetComponent<Rigidbody2D>().position.x;
			//climb.baseLink.connectedBody = collider.transform.parent.GetComponent<Rigidbody2D>();

			attach = true;
		}
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		if (collider.gameObject.layer == 9) 
		{
			climb.xPos = 0;
			attach = false;

		}
	}

}
