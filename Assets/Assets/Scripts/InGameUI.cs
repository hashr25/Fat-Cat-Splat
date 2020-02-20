using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour {
	Text scoreText;
	Text highScoreText;

	void Start(){
		scoreText = GameObject.Find ("ScoreCounter").GetComponent<Text> ();
		highScoreText = GameObject.Find ("HighScoreCounter").GetComponent<Text> ();
		SetHighScore ();
	}

	void Update(){
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = ScoreTracker.scoreTracker.currentScore.ToString();
	}

	void SetHighScore(){
		if (SceneManager.GetActiveScene().name == "GrassChapter") {
			highScoreText.text = ScoreTracker.scoreTracker.grassChapterHighScore.ToString ();
		} else if (SceneManager.GetActiveScene().name == "MistChapter") {
			highScoreText.text = ScoreTracker.scoreTracker.mistChapterHighScore.ToString ();
		}
	}

	public void OnResetLevelButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void OnMainMenuButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		SceneManager.LoadScene ("TitleScreen");
	}
}
