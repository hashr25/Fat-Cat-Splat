using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour {

	public static InGameUI inGameUI;

	Text scoreText;
	Text highScoreText;
	GameObject scoreMultiplier;
	Text scoreMultiplierText;

	bool hasBrokenHighScore = false;

	bool scoreMultiplierToggled = false;

	void Start(){
		inGameUI = this;

		scoreText = GameObject.Find ("ScoreCounter").GetComponent<Text> ();
		highScoreText = GameObject.Find ("HighScoreCounter").GetComponent<Text> ();
		scoreMultiplierText = GameObject.Find("ScoreMultiplierCounter").GetComponent<Text>();
		SetHighScore ();

		scoreMultiplier = GameObject.Find("ScoreMultiplier");
		scoreMultiplier.SetActive(false);
	}

	void Update(){
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = ScoreTracker.scoreTracker.currentScore.ToString();
        if(int.Parse(scoreText.text) > int.Parse(highScoreText.text)) { hasBrokenHighScore = true; }
	}

    public void UpdateScoreMultiplier()
    {
		int currentScoreMultiplier = ScoreTracker.scoreTracker.currentScoreMultiplier;
		scoreMultiplierText.text = currentScoreMultiplier.ToString();

        if (!scoreMultiplierToggled && currentScoreMultiplier > 0)
        {
			scoreMultiplier.SetActive(true);
			scoreMultiplierToggled = true;
        }
    }

	void SetHighScore(){
		if (SceneManager.GetActiveScene().name == "GrassChapter") {
			highScoreText.text = ScoreTracker.scoreTracker.grassChapterHighScore.ToString ();
		} else if (SceneManager.GetActiveScene().name == "MistChapter") {
			highScoreText.text = ScoreTracker.scoreTracker.mistChapterHighScore.ToString ();
		}
	}

	public void OnResetLevelButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void OnMainMenuButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		SceneManager.LoadScene ("TitleScreen");
	}

    public void PlayerDied()
    {
		DialogSpawner.dialogSpawner.SpawnConfirmationDialog("Would you like to play this stage again?",
            () => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); },
            () => { SceneManager.LoadScene("TitleScreen"); } );
        if (hasBrokenHighScore)
        {
			DialogSpawner.dialogSpawner.SpawnErrorDialog("You have beat your high score! Great job!");
        }
    }
}
