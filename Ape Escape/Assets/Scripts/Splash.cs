using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	private Motor player;
	private bool splashDelay;

	public GameObject splashParticle;

	public float splashDelayTime = 0.5f;



	// Use this for initialization
	void Start () {

		player = FindObjectOfType<Motor>();


	}
		
	void OnTriggerEnter2D(Collider2D collider)
	{

		Instantiate (splashParticle, new Vector2(player.GetComponent<Rigidbody2D>().position.x, player.GetComponent<Rigidbody2D>().position.y - 0.5f), splashParticle.transform.rotation);

	}

	void OnTriggerStay2D(Collider2D collider)
	{

		if (this.gameObject.tag == "Mud" && collider.GetComponent<Rigidbody2D> ().velocity.x != 0 && !splashDelay)
			StartCoroutine (SplashCO ());
	}


	IEnumerator SplashCO ()
	{
		splashDelay = true;
		Instantiate (splashParticle, new Vector2(player.GetComponent<Rigidbody2D>().position.x, player.GetComponent<Rigidbody2D>().position.y - 0.5f), splashParticle.transform.rotation);

		yield return new WaitForSeconds(splashDelayTime);

		splashDelay = false;
	}
}
