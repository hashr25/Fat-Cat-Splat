using UnityEngine;
using System.Collections;

public class SimpleCameraFollow : MonoBehaviour {
	public float xOffset = 5f;
	public float yOffset = 0f;

	GameObject cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		cam.transform.position = new Vector3 (transform.position.x+xOffset, yOffset, -10f);
	}
}
