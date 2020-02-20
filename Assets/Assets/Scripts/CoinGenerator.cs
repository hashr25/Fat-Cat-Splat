using UnityEngine;
using System.Collections;

public class CoinGenerator : MonoBehaviour {

	public GameObject coinPrefab;
	public GameObject player;

	public float minCoinHeight = -3.4f;
	public float maxCoinHeight = 4.7f;

	public float minTimeBetweenCoins = 5.0f;
	public float maxTimeBetweenCoins = 10.5f;

	float timeTilNextCoin;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		timeTilNextCoin = (float)Random.Range (minTimeBetweenCoins * 100, maxTimeBetweenCoins * 100) / 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeTilNextCoin <= 0) {
			SpawnCoin ();
			timeTilNextCoin = (float)Random.Range (minTimeBetweenCoins * 100, maxTimeBetweenCoins * 100) / 100;
		} else {
			timeTilNextCoin -= Time.deltaTime;
		}
	}

	void SpawnCoin(){
		float coinY = (float)Random.Range (minCoinHeight * 100, maxCoinHeight * 100) / 100;
		float coinX = player.transform.position.x + 20;

		GameObject spawnedCoin = Instantiate (coinPrefab);
		spawnedCoin.transform.position = new Vector2 (coinX, coinY);
	}
}
