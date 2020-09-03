using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	public float backgroundMovementSpeed = 2.5f;
	public float fullSceneWidth = 327.48f;
	public float groundShift = 81.92f; //or 81.87 ???

	public GameObject[] scenes; 
	public GameObject ground;
	int sceneToMove = 0;
	bool firstScenePassed;

	// Use this for initialization
	void Start () {
		firstScenePassed = false;
	}
	
	// Update is called once per frame
	void Update () {
        //Moving background to create paralax effect.
		transform.Translate (Time.deltaTime * backgroundMovementSpeed, 0, 0);

	}

	public void ShiftBackground(){
		//float groundStartX = ground.transform.position.x - (410.0f * 1.4f);
		//float groundEndX = ground.transform.position.x;
		//float playerX = GameObject.Find("Player").transform.position.x;

		//Debug.Log("Before Move: Ground behind the Player: " + (playerX - groundStartX));
		//Debug.Log("Before Move: Ground in front of Player: " + (groundEndX - playerX));


		//Core Functionality
		if (firstScenePassed) {
			scenes [sceneToMove % 4].transform.position = new Vector3((scenes [sceneToMove % 4].transform.position.x + fullSceneWidth), 0f, 0f);
			ground.transform.position = new Vector3 ((ground.transform.position.x + (fullSceneWidth/2.35f)), -3.6f, 0f);
			sceneToMove++;
		} else {
			firstScenePassed = true;
			ground.transform.position = new Vector3((ground.transform.position.x + fullSceneWidth/6), -3.6f, 0f);
		}
		//end

		//groundStartX = ground.transform.position.x - (410.0f * 1.4f);
		//groundEndX = ground.transform.position.x;
		//playerX = GameObject.Find("Player").transform.position.x;

		//Debug.Log("After Move: Ground behind the Player: " + (playerX - groundStartX));
		//Debug.Log("After Move: Ground in front of Player: " + (groundEndX - playerX));
	}
}
