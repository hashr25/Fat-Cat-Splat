using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsScreenUI : MonoBehaviour {

	public void OnBackButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		SceneManager.LoadScene ("Settings");
	}

	public void OnArtistButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		Application.OpenURL ("http://www.bevouliin.com");
	}

	public void OnMusicButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		Application.OpenURL ("http://www.sleepfacingwest.com");
	}

	public void OnFontButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		Application.OpenURL ("http://www.brittneymurphydesign.com");
	}
}
