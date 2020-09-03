using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if (SceneManager.GetActiveScene().name == "GrassChapter") {
			print ("Passed the Grass Chapter!");
			GameController.gameController.grassChapterPassed = true;
		} else if (SceneManager.GetActiveScene().name == "MistChapter") {
			print ("Passed the Mist Chapter!");
			GameController.gameController.mistChapterPassed = true;
		}
	}
}
