using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour {
	Text titleText;

	public float changeTimer = 0.075f;
	public bool isStoreEnabled = false;
	float timeToChangeText;

	public GameObject errorDialogPrefab;
	public GameObject confirmationDialogPrefab;

	// Use this for initialization
	void Start () {
		titleText = GameObject.Find ("Title").GetComponent<Text> ();
		timeToChangeText = changeTimer;

		GameObject.Find ("CoinCount").GetComponent<Text> ().text = GameController.gameController.coins.ToString();

		Color greyed = new Color(0.75f, 0.75f, 0.75f, 0.8f);
		//Grey out mist chapter button
		if (!GameController.gameController.grassChapterPassed) {
			

			GameObject mistChapterButton = GameObject.Find("MistChapterButton");
			//ColorBlock buttonColors = mistChapterButton.GetComponent<Button>().colors;

			//mistChapterButton.GetComponent<Button>().enabled = false;
			mistChapterButton.GetComponent<Button>().image.color = greyed;
			mistChapterButton.GetComponentInChildren<Text> ().color = greyed;
		}

        //Grey out store button
        if (!isStoreEnabled)
        {
			GameObject storeButton = GameObject.Find("StoreButton");
			//ColorBlock buttonColors = mistChapterButton.GetComponent<Button>().colors;

			//mistChapterButton.GetComponent<Button>().enabled = false;
			storeButton.GetComponent<Button>().image.color = greyed;
			storeButton.GetComponentInChildren<Text>().color = greyed;
		}

        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
			GameObject leaderboardButton = GameObject.Find("Leaderboards");
			Destroy(leaderboardButton);
        }
	}

	public void OnGrassChapterButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource> ().Play (); }
        
		SceneManager.LoadScene ("GrassChapter");
	}

	public void OnMistChapterButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		if (GameController.gameController.grassChapterPassed) {
			SceneManager.LoadScene ("MistChapter");
		} else {
			DialogSpawner.dialogSpawner.SpawnErrorDialog("You must complete the Grass Chapter before moving on to the Mist Chapter.");
		}
	}

	public void OnCharacterStoreButtonClicked()
	{
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		if (isStoreEnabled)
		{
			//Load Store Scene
			DialogSpawner.dialogSpawner.SpawnErrorDialog("Something really fucked up happened. The store was supposed to be disabled...");
		}
		else
		{
			///Display error?
			DialogSpawner.dialogSpawner.SpawnErrorDialog("The character store is not yet available.");
            
		}
	}

	public void OnLeaderboardButtonClicked(){
        if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{ 
			//SceneManager.LoadScene ("Leaderboard");
			SocialController.socialController.ShowLeaderboards();
		}
        else
        {
			DialogSpawner.dialogSpawner.SpawnErrorDialog("The leaderboards are only currently available on mobile platforms.");
		}
		
	}

	public void OnSettingsMenuButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		SceneManager.LoadScene ("Settings");
	}

	public void OnFacebookButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		string facebookUrl = "http://www.facebook.com/HotHashGames/";

		if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            //Find some other way to open URLS so that it 
			Application.OpenURL(facebookUrl);
		}
	}

	public void OnTwitterButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		string twitterUrl = "http://www.twitter.com/HotHashGames/";

		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			//Find some other way to open URLS so that it 
			Application.OpenURL(twitterUrl);
		}
	}
}
