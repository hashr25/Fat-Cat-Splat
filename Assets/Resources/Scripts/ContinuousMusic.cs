using UnityEngine;
using System.Collections;

public class ContinuousMusic : MonoBehaviour {

	public static ContinuousMusic music;

	// Use this for initialization
	void Awake () {
		if (music == null) {
			DontDestroyOnLoad (gameObject);
			music = this;
		} else if(music != this) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
