using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataIO
{
	public static bool Save<T>(string key, object value)
    {
        bool success;

		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
            try
            {
				MonoBehaviour.print("Trying to save object with key: " + key);
				string json = JsonUtility.ToJson(value);
				MonoBehaviour.print("JSON: " + json);
				string encryptedData = Encrypt.EncryptString(json, HashKey.hashKey);
				MonoBehaviour.print("Encrypted: " + encryptedData);

				PlayerPrefs.SetString(key, encryptedData);
				PlayerPrefs.Save();
				MonoBehaviour.print("Called PlayerPrefs.Save();");

				success = true;
			}
			catch(Exception e)
            {
				MonoBehaviour.print("Error saving data for WebGL.");
				MonoBehaviour.print(e.Message);

				success = false;
            }
		}
		else
		{
            try
            {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Create(Application.persistentDataPath + "/" + key + ".dat");

				bf.Serialize(file, value);
				file.Close();

				success = true;
			}
            catch(Exception e)
            {
				MonoBehaviour.print("Error saving data for Mobile.");
				MonoBehaviour.print(e.Message);

				success = false;
			}
			
		}

		return success;
    }

    public static T Load<T>(string key)
    {
        T objectValue = default(T);

		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
            try
            {
				MonoBehaviour.print("Trying to load PlayerSave");
				string encryptedSaveData = PlayerPrefs.GetString(key);
				MonoBehaviour.print("Encrypted Data: " + encryptedSaveData);

				if (!String.IsNullOrEmpty(encryptedSaveData))
				{
					string json = Encrypt.DecryptString(encryptedSaveData, HashKey.hashKey);
					MonoBehaviour.print("JSON: " + json);
					T data = JsonUtility.FromJson<T>(json);

					objectValue = data;
				}
			}
            catch(Exception e)
            {
				MonoBehaviour.print("Error loading data for WebGL.");
				MonoBehaviour.print(e.Message);
			}
			
		}
		else
		{
            try
            {
				if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
				{
					BinaryFormatter bf = new BinaryFormatter();
					FileStream file = File.Open(Application.persistentDataPath + "/" + key + ".dat", FileMode.Open);

					T data = (T)bf.Deserialize(file);
					file.Close();

					objectValue = data;
				}
			}
            catch(Exception e)
            {
				MonoBehaviour.print("Error loading data for Mobile.");
				MonoBehaviour.print(e.Message);
			}
			
		}

		return objectValue;
    }
}
