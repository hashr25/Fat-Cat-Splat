using UnityEngine;
using System.Collections;
using System.Linq;

public class ObstacleManager : MonoBehaviour
{
	public GameObject player;

	public float minTimeBetweenObstacles = 3;
	public float maxTimeBetweenObstacles = 5;
	float timeTilNextObstacle;
	bool pushBackTime = false;

	public GameObject[] sceneOneObstacles;
	public GameObject[] sceneTwoObstacles;
	public GameObject[] sceneThreeObstacles;
	public GameObject[] sceneFourObstacles;
	public GameObject[] allObstacles;

	public int currentScene = 0;
	public float pencilPushbackTime = 1.5f;
	public float obstaclePushbackOffset = 20f;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		timeTilNextObstacle = (float)Random.Range (minTimeBetweenObstacles * 100, maxTimeBetweenObstacles * 100) / 100;
		//print("Time before next obstacles: " + timeTilNextObstacle.ToString());
		FindAllObstacles ();
	}
	
	// Update is called once per frame
	void Update () {
		if (timeTilNextObstacle <= 0) {
			SpawnNextObject ();
			timeTilNextObstacle = (float)Random.Range (minTimeBetweenObstacles * 100, maxTimeBetweenObstacles * 100) / 100;
            if (pushBackTime) { timeTilNextObstacle += pencilPushbackTime; }
		} else {
			timeTilNextObstacle -= Time.deltaTime;
		}
	}

	void SpawnNextObject () {
		GameObject[] sceneObjects;

		if (currentScene == 0) {
			sceneObjects = sceneOneObstacles;
		} else if (currentScene == 1) {
			sceneObjects = sceneTwoObstacles;
		} else if (currentScene == 2) {
			sceneObjects = sceneThreeObstacles;
		} else if (currentScene == 3) {
			sceneObjects = sceneFourObstacles;
		} else {
			sceneObjects = allObstacles;
		}

		bool goodChoice = false;
		GameObject currentSpawningObject = sceneObjects [Random.Range (0, sceneObjects.Length)];

		while (!goodChoice && currentSpawningObject) {
			if (currentSpawningObject.transform.position.x < player.transform.position.x - 5) {
				goodChoice = true;
			} else {
				currentSpawningObject = sceneObjects [Random.Range (0, sceneObjects.Length)];
			}
		}

		//Them actually getting spawned... 
		if (currentSpawningObject) {
			float yPosition = currentSpawningObject.transform.position.y;

			if (yPosition < -5) {
				yPosition = -2.5f;
			}

			currentSpawningObject.transform.position = new Vector2 (player.transform.position.x + obstaclePushbackOffset, yPosition);
		}

        if(currentSpawningObject.name.Contains("PencilBundle"))
        {
			pushBackTime = true;
        }
        else
        {
			pushBackTime = false;
        }
	}

	void FindAllObstacles(){
		allObstacles = sceneOneObstacles.Concat(sceneTwoObstacles).Concat(sceneThreeObstacles).Concat(sceneFourObstacles).ToArray();
	}
}
