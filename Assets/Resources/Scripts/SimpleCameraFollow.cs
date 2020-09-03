using UnityEngine;
using System.Collections;

public class SimpleCameraFollow : MonoBehaviour {
	public float xOffset = 5f;
	public float yOffset = 0.05f;

	public GameObject cam;

	// Use this for initialization
	void Awake () {
		if(cam == null)
        {
			cam = GameObject.Find("Main Camera");
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (cam == null)
		{
			cam = GameObject.Find("Main Camera");
		}
		cam.transform.position = new Vector3 (transform.position.x+xOffset, yOffset, -10f);
	}
}
