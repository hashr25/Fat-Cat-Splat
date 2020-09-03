using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;

public class GameController : MonoBehaviour {

	public static GameController gameController;

	//Player Data to save
	public int coins = 0;
	public bool grassChapterPassed = false;
	public bool mistChapterPassed = false;

	// Use this for initialization
	void Awake () {
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			Load ();
			gameController = this;
		} else if(gameController != this) {
			Destroy (gameObject);
		}
	}

	void OnDestroy(){
		//Do not save on destroy. For some reason when OnDestroy is called
		//All variables already are written back to default values( 0 or false )
		//Save ();
	}

	public void StopBackground () {
		GameObject.Find("Background").GetComponent<BackgroundManager> ().backgroundMovementSpeed = 0;
	}

	public void Save() {
		PlayerData data = new PlayerData(coins, grassChapterPassed, mistChapterPassed);
		DataIO.Save<PlayerData>("PlayerSave", data);
	}

	public void Load() {
		PlayerData data = DataIO.Load<PlayerData>("PlayerSave");
        if(data != null)
        {
			coins = data.coins;
			grassChapterPassed = data.grassChapterPassed;
			mistChapterPassed = data.mistChapterPassed;
		}
        
	}
}

[Serializable]
class PlayerData{

	public int coins;
	public bool grassChapterPassed = false;
	public bool mistChapterPassed = false;

	public PlayerData (int coin, bool grass, bool mist){
		coins = coin;
		grassChapterPassed = grass;
		mistChapterPassed = mist;
	}
}