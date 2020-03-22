using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	public float backgroundMovementSpeed = 2.5f;
	public float fullSceneWidth = 327.48f;
	public float groundShift = 81.87f;

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
			scenes [sceneToMove % 4].transform.position = new Vector3((scenes [sceneToMove % 4].transform.position.x + fullSceneWidth), 0f, 0f);
			ground.transform.position = new Vector3 ((ground.transform.position.x + (groundShift * 2)), -3.6f, 0f);
			sceneToMove++;
		} else {
			firstScenePassed = true;
			ground.transform.position = new Vector3((ground.transform.position.x + groundShift), -3.6f, 0f);
		}
	}
}
