// Attach to camera

using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxScales;
	public float xSmoothing;
	public float ySmoothing;

	private Vector3 prevCamPos;


	void Start () 
	{

		prevCamPos = transform.position;

		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < parallaxScales.Length; i++) 
			parallaxScales[i] = backgrounds[i].position.z * -1;

	}


	void LateUpdate () 
	{
		for (int i = 0; i < backgrounds.Length; i++) 
		{
			float xParallax = (prevCamPos - transform.position).x * (parallaxScales[i] / xSmoothing);

			float yParallax = (prevCamPos - transform.position).y * (parallaxScales[i] / ySmoothing);

			backgrounds[i].position = new Vector3 (backgrounds[i].position.x + xParallax, backgrounds[i].position.y + yParallax, backgrounds[i].position.z);
		}

		prevCamPos = transform.position;
	}
}
