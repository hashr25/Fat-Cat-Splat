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

	private string hashKey = "The value of life is only what you make of it.";

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
		if (true/*Application.platform == RuntimePlatform.WebGLPlayer*/)
		{
			print("Trying to save PlayerData:");
            PlayerData data = new PlayerData(coins, grassChapterPassed, mistChapterPassed);
			string json = JsonUtility.ToJson(data);
			print("JSON: " + json);
			string encryptedData = Encrypt.EncryptString(json, hashKey);
			print("Encrypted: " + encryptedData);

			PlayerPrefs.SetString("PlayerSave", encryptedData);
			PlayerPrefs.Save();
			print("Called PlayerPrefs.Save();");
		}
		else
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

			PlayerData data = new PlayerData(coins, grassChapterPassed, mistChapterPassed);

			bf.Serialize(file, data);
			file.Close();
		}
	}

	public void Load() {
		if (true/*Application.platform == RuntimePlatform.WebGLPlayer*/)
		{
			print("Trying to load PlayerSave");
			string encryptedSaveData = PlayerPrefs.GetString("PlayerSave");
			print("Encrypted Data: " + encryptedSaveData);

            if (!String.IsNullOrEmpty(encryptedSaveData))
            {
				string json = Encrypt.DecryptString(encryptedSaveData, hashKey);
				print("JSON: " + json);
				PlayerData data = JsonUtility.FromJson<PlayerData>(json);

				coins = data.coins;
				grassChapterPassed = data.grassChapterPassed;
				mistChapterPassed = data.mistChapterPassed;
			}
			
		}
		else
		{
			if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
			{
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

				PlayerData data = (PlayerData)bf.Deserialize(file);
				file.Close();

				coins = data.coins;
				grassChapterPassed = data.grassChapterPassed;
				mistChapterPassed = data.mistChapterPassed;
			}
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