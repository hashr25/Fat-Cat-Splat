using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float selfDestructTime = 5.0f;

	float timeTilDestruct;

	// Use this for initialization
	void Start () {
		timeTilDestruct = selfDestructTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeTilDestruct <= 0) {
			Destroy (gameObject);
		} else {
			timeTilDestruct -= Time.deltaTime;
		}
	}
}
