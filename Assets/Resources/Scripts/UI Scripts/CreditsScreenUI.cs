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

	public void OnHiddenPromoCodeButtonClicked()
    {
		if(!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		DialogSpawner.dialogSpawner.SpawnCodeEntryBox("Enter your promo code here:",
			() =>
			{
				string promoCode = GameObject.Find("InputField").GetComponentsInChildren<Text>()[1].text;
				PromoCodeLibrary.promoCodeLibrary.UsePromoCode(promoCode);
			}, null); // No action for decline right now...
    }
}
