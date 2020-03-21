using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	public float backgroundMovementSpeed = 2.5f;

	public GameObject[] scenes; 
	public GameObject ground;
	int sceneToMove = 0;
	bool firstScenePassed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Moving background to create paralax effect.
		transform.Translate (Time.deltaTime * backgroundMovementSpeed, 0, 0);

	}

	public void ShiftBackground(){
		if (firstScenePassed) {
			scenes [sceneToMove % 4].transform.position = new Vector3((scenes [sceneToMove % 4].transform.position.x+327.68f), 0f, 0f);
			ground.transform.position = new Vector3 ((ground.transform.position.x + 81.92f), -3.6f, 0f);
			sceneToMove++;
		} else {
			firstScenePassed = true;
		}
	}
}
