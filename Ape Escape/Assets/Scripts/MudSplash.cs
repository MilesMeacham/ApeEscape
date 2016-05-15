using UnityEngine;
using System.Collections;

public class MudSplash : MonoBehaviour {

	private Motor player;
	private bool splashDelay;

	public GameObject mudParticle;

	public float splashDelayTime = 0.5f;



	// Use this for initialization
	void Start () {

		player = FindObjectOfType<Motor>();


	}
		
	void OnTriggerEnter2D(Collider2D collider)
	{

		Instantiate (mudParticle, new Vector2(player.GetComponent<Rigidbody2D>().position.x, player.GetComponent<Rigidbody2D>().position.y - 0.5f), mudParticle.transform.rotation);

	}

	void OnTriggerStay2D(Collider2D collider)
	{

		if (collider.GetComponent<Rigidbody2D> ().velocity.x != 0 && !splashDelay)
			StartCoroutine (SplashCO ());
	}


	IEnumerator SplashCO ()
	{
		splashDelay = true;
		Instantiate (mudParticle, new Vector2(player.GetComponent<Rigidbody2D>().position.x, player.GetComponent<Rigidbody2D>().position.y - 0.5f), mudParticle.transform.rotation);

		yield return new WaitForSeconds(splashDelayTime);

		splashDelay = false;
	}
}
