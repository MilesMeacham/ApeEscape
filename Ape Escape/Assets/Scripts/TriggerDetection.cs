using UnityEngine;
using System.Collections;

public class TriggerDetection : MonoBehaviour {

	private GameManager gameManager;

	void Start ()
	{
		// I Can use this method since there is only one gameManager
		// FYI it is not always great to use this method. It has to look through many gameobjects when the scene gets big
		gameManager = FindObjectOfType<GameManager> ();

	}


	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject.layer == 10)
			gameManager.RespawnPlayer ();


	}

}
