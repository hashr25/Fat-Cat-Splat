using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class AudioManager : MonoBehaviour {

	static public AudioManager audioManager;

	public AudioSource music;

	public AudioClip mainTheme;
	public AudioClip mistTheme;

	// Use this for initialization
	void Awake () {
		if (audioManager == null) {
			DontDestroyOnLoad (gameObject);
			Load ();
			audioManager = this;
			music = GetComponent<AudioSource> ();
		} else if(audioManager != this) {
			Destroy (gameObject);
		}
	}

	void OnDestroy(){
		Save ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLevelWasLoaded(int level) {
		if (level == 2) {
			SetMistTheme ();
		} else {
			SetMainTheme ();
		}
	}

	public void SetVolume(float v){
		music.volume = v;
	}

	public void TurnOnMusic(){
		music.mute = false;
	}

	public void TurnOffMusic(){
		music.mute = true;
	}

	public void SetMainTheme(){
		if (music.clip != mainTheme) {
			music.clip = mainTheme;
			music.Play ();
		}
	}

	public void SetMistTheme(){
		if (music.clip != mistTheme) {
			music.clip = mistTheme;
			music.Play ();
		}
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/soundInfo.dat");

		SoundData data = new SoundData(music.mute, music.volume);

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/soundInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/soundInfo.dat", FileMode.Open);

			SoundData data = (SoundData)bf.Deserialize (file);
			file.Close ();

			music.mute = data.mute;
			music.volume = data.soundVol;
		}
	}
}

[Serializable]
class SoundData{

	public bool mute;
	public float soundVol;

	public SoundData(bool muteFlag, float soundV){
		mute = muteFlag;
		soundVol = soundV;
	}

}