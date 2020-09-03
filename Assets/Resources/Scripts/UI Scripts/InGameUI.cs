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

	float chanceOfRewardAdAfterRun = 25.0f;
	float chanceOfAdAfterRun = 5.0f;
	float chanceOfDonationAsked = 2.0f;

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
		AskPlayAgain();
        if (!PromptForAdShow())
        {
            if (!ShowGeneralAd())
            {

            }
        }
		ShowHighScoreBroken();

	}

	private void AskPlayAgain()
    {
		DialogSpawner.dialogSpawner.SpawnConfirmationDialog("Would you like to play this stage again?",
			() => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); },
			() => { SceneManager.LoadScene("TitleScreen"); });
	}

	private void ShowHighScoreBroken()
    {
		if (hasBrokenHighScore)
		{
			DialogSpawner.dialogSpawner.SpawnErrorDialog("You have beat your high score! Great job!");
		}
	}

	private bool PromptForAdShow()
    {
		bool adOffered = false;

        if (AdsManager.adsManager.IsRewardAdReady())
        {
			if (Random.Range(0.0f, 100.0f) <= chanceOfRewardAdAfterRun)
            {
				DialogSpawner.dialogSpawner.SpawnConfirmationDialog("Do you want to watch an advertisement for 100 coins?",
					() => { AdsManager.adsManager.RunGoldRewardAd(100); },
					() => { DialogSpawner.dialogSpawner.SpawnErrorDialog("Okay! No Reward Ad will be show!"); });

				adOffered = true;
			}
				
		}

		return adOffered;
    }

	private bool ShowGeneralAd()
    {
		bool adShown = false;

		if (Random.Range(0.0f, 100.0f) <= chanceOfAdAfterRun)
		{
			AdsManager.adsManager.RunSimpleAd();
			adShown = true;
		}

		return adShown;
	}

    private void RequestDonation()
    {
		if(Random.Range(0.0f, 100.0f) <= chanceOfDonationAsked)
        {
			DialogSpawner.dialogSpawner.SpawnConfirmationDialog("Would you like to support the developer by making a secure donation to us via PayPal?",
					() =>
					{
						string donateUrl = "https://bit.ly/HotHashGames";

						if (Application.platform != RuntimePlatform.WebGLPlayer)
						{
							//Find some other way to open URLS so that it 
							Application.OpenURL(donateUrl);
						}
						else
						{
							Application.OpenURL(donateUrl);
						}
					},
					() => { DialogSpawner.dialogSpawner.SpawnErrorDialog("Okay! No worries! Thanks so much for playing our game!"); });
		}
    }
}
