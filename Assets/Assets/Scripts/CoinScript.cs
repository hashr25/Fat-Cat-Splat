using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		print ("Someting is hitting the coin.");
		GameController.gameController.coins += ScoreTracker.scoreTracker.currentScoreMultiplier;//Add coins * score multipler
		ScoreTracker.scoreTracker.CatchCoin ();
		Destroy (gameObject);
	}
}
