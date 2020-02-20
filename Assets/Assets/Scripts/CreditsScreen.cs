using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsScreen : MonoBehaviour {

	public void OnBackButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		SceneManager.LoadScene ("Settings");
	}

	public void OnArtistButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		Application.OpenURL ("http://www.bevouliin.com");
	}

	public void OnMusicButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		Application.OpenURL ("http://www.sleepfacingwest.com");
	}

	public void OnFontButtonClicked(){
		GetComponent<AudioSource> ().Play ();
		Application.OpenURL ("http://www.brittneymurphydesign.com");
	}
}
