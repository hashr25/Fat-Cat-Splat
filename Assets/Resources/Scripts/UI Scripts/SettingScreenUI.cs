using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingScreenUI : MonoBehaviour {
	Text titleText;

	public float changeTimer = 0.075f;
	float timeToChangeText;

	bool soundEnabled = true;

	Button soundOn;
	Button soundOff;

	// Use this for initialization
	void Start () {
		soundOn = GameObject.Find ("SoundOn").GetComponent<Button> ();
		soundOff = GameObject.Find ("SoundOff").GetComponent<Button> ();

		titleText = GameObject.Find ("Title").GetComponent<Text> ();
		timeToChangeText = changeTimer;

		GetComponentInChildren<Slider> ().value = AudioManager.audioManager.music.volume;

		if (AudioManager.audioManager.music.mute) {
			OnSoundOffButtonClicked ();
		} else {
			OnSoundOnButtonClicked ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void OnSoundOnButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		if (!soundEnabled) {
			AudioManager.audioManager.TurnOnMusic ();
			soundEnabled = true;



			Color normal = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			Color greyed = new Color (0.5f, 0.5f, 0.5f, 0.5f);

			ColorBlock soundOnColors = soundOn.colors;
			ColorBlock soundOffColors = soundOff.colors;

			soundOnColors.normalColor = normal;
			soundOffColors.normalColor = greyed;

			soundOn.colors = soundOnColors;
			soundOff.colors = soundOffColors;
		}
	}

	public void OnSoundOffButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		if (soundEnabled) {
			AudioManager.audioManager.TurnOffMusic ();
			soundEnabled = false;

			Color normal = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			Color greyed = new Color (0.5f, 0.5f, 0.5f, 0.5f);

			ColorBlock soundOnColors = soundOn.colors;
			ColorBlock soundOffColors = soundOff.colors;

			soundOnColors.normalColor = greyed;
			soundOffColors.normalColor = normal;

			soundOn.colors = soundOnColors;
			soundOff.colors = soundOffColors;
		}
	}

	public void OnVolumeSliderChanges(){
		AudioManager.audioManager.SetVolume(GetComponentInChildren<Slider> ().value);
	}

	public void OnBackButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		SceneManager.LoadScene ("TitleScreen");
	}

	public void OnCreditsButtonClicked(){
		if (!AudioManager.audioManager.mute) { GetComponent<AudioSource>().Play(); }
		SceneManager.LoadScene ("Credits");
	}
}
