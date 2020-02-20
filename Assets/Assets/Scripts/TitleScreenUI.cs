using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour {
	Text titleText;

	public float changeTimer = 0.075f;
	float timeToChangeText;

	// Use this for initialization
	void Start () {
		titleText = GameObject.Find ("Title").GetComponent<Text> ();
		timeToChangeText = changeTimer;

		GameObject.Find ("CoinCount").GetComponent<Text> ().text = GameController.gameController.coins.ToString();

		//Grey out mist chapter button
		if (!GameController.gameController.grassChapterPassed) {
			Color greyed = new Color (0.5f, 0.5f, 0.5f, 0.5f);

			GameObject mistChapterButton = GameObject.Find("MistChapterButton");
			//ColorBlock buttonColors = mistChapterButton.GetComponent<Button>().colors;

			mistChapterButton.GetComponent<Button>().enabled = false;
			mistChapterButton.GetComponent<Button>().image.color = greyed;
			mistChapterButton.GetComponentInChildren<Text> ().color = greyed;

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnGrassChapterButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		SceneManager.LoadScene ("GrassChapter");
	}

	public void OnMistChapterButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		if (GameController.gameController.grassChapterPassed) {
			SceneManager.LoadScene ("MistChapter");
		} else {
			///Display error?
		}
	}

	public void OnLeaderboardButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		//SceneManager.LoadScene ("Leaderboard");
		SocialController.socialController.ShowLeaderboards();
	}

	public void OnSettingsMenuButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		SceneManager.LoadScene ("Settings");
	}

	public void OnFacebookButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		string facebookUrl = "http://www.facebook.com/HotHashGames/";

		if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
			Application.ExternalEval("window.open(\"" + facebookUrl + ",\"_blank\")");
		}
        else
		{
			Application.OpenURL(facebookUrl);
		}
	}

	public void OnTwitterButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		string twitterUrl = "http://www.twitter.com/HotHashGames/";

		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			Application.ExternalEval("window.open(\"" + twitterUrl + ",\"_blank\")");
		}
		else
		{
			Application.OpenURL(twitterUrl);
		}
	}
}
