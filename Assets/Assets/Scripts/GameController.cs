using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
		Save ();
	}

	public void StopBackground () {
		GameObject.Find("Background").GetComponent<BackgroundManager> ().backgroundMovementSpeed = 0;
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData(coins, grassChapterPassed, mistChapterPassed);

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

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