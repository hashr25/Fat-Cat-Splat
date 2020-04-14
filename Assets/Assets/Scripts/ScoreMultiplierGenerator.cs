using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplierGenerator : MonoBehaviour
{
	public GameObject multiplierPrefab;
	public GameObject player;

	public float minCoinHeight = -3.4f;
	public float maxCoinHeight = 4.7f;

	public float minTimeBetweenMultipliers = 15.0f;
	public float maxTimeBetweenMultipliers = 30.0f;

	float timeTilNextMultiplier;

	// Use this for initialization
	void Start()
	{
		player = GameObject.Find("Player");
		timeTilNextMultiplier = (float)Random.Range(minTimeBetweenMultipliers * 100, maxTimeBetweenMultipliers * 100) / 100;
	}

	// Update is called once per frame
	void Update()
	{
		if (timeTilNextMultiplier <= 0)
		{
			SpawnCoin();
			timeTilNextMultiplier = (float)Random.Range(minTimeBetweenMultipliers * 100, maxTimeBetweenMultipliers * 100) / 100;
		}
		else
		{
			timeTilNextMultiplier -= Time.deltaTime;
		}
	}

	void SpawnCoin()
	{
		float multiplierY = (float)Random.Range(minCoinHeight * 100, maxCoinHeight * 100) / 100;
		float multiplierX = player.transform.position.x + 20;

		GameObject spawnedCoin = Instantiate(multiplierPrefab);
		spawnedCoin.transform.position = new Vector2(multiplierX, multiplierY);
	}
}
