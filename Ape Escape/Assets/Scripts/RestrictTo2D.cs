using UnityEngine;
using System.Collections;

public class RestrictTo2D : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	//prevents the object from moving and rotating in a way unfit for 2D

	void FixedUpdate () {
		//transform.localPosition.z = 0;
		transform.eulerAngles = new Vector3 (0, 0);
	}
}
