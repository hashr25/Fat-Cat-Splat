using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

	static public AudioManager audioManager;

	public AudioSource music;
	public bool mute;

	public AudioClip mainTheme;
	public AudioClip mistTheme;

	// Use this for initialization
	void Awake () {
		if (audioManager == null) {
			SceneManager.sceneLoaded += OnSceneLoaded;
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
		if(scene.name == "MistChapter")
        {
			SetMistTheme();
        }
        else
        {
			SetMainTheme();
        }
    }

	//void OnLevelWasLoaded(int level) {
	//	if (level == 2) {
	//		SetMistTheme ();
	//	} else {
	//		SetMainTheme ();
	//	}
	//}

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
		SoundData data = new SoundData(music.mute, music.volume);
		DataIO.Save<SoundData>("SoundSave", data);
	}

	public void Load(){
		SoundData data = DataIO.Load<SoundData>("SoundSave");
		if (data != null)
		{
			mute = data.mute;
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