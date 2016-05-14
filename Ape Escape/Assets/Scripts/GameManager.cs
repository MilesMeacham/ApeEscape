using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private GameObject player;
	public GameObject checkpoint;

	public float respawnDelay = 1f;


	void Start () 
	{
		player = GameObject.FindWithTag ("Player");
	}
	
	public void RespawnPlayer ()
	{
		StartCoroutine (RespawnPlayerCo ());

	}

	IEnumerator RespawnPlayerCo ()
	{
		player.SetActive (false);
		player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

		yield return new WaitForSeconds(respawnDelay);

		player.transform.position = checkpoint.transform.position;
		player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		player.SetActive (true);

	}

}
