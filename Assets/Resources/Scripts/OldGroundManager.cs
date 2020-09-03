using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldGroundManager : MonoBehaviour
{
	public GameObject[] groundBlocks;
	public float timeBetweenMoves = 10f;
	public float sizeOfBlock = 18.76f;

	float timeRemaining;
	int currentBlockToMove = 0;

	// Use this for initialization
	void Start()
	{
		timeRemaining = timeBetweenMoves;
	}

	// Update is called once per frame
	void Update()
	{
		if (timeRemaining <= 0)
		{
			timeRemaining = timeBetweenMoves;
			print("Ground Blocks Length: " + groundBlocks.Length.ToString());
			float distanceMoved = sizeOfBlock * (groundBlocks.Length);
			print("Distance Moved: " + distanceMoved.ToString());
			groundBlocks[currentBlockToMove % groundBlocks.Length].transform.Translate(distanceMoved, 0f, 0f);
			currentBlockToMove++;
			print("Moving block of ground");
		}
		else
		{
			timeRemaining -= Time.deltaTime;
		}
	}
}
