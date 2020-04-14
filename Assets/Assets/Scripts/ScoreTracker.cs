using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour {
	public static ScoreTracker scoreTracker;

	public long currentScore;
	public int currentScoreMultiplier;
	bool active;

	public long grassChapterHighScore;
	public long mistChapterHighScore;

	private string hashKey = "The value of life is only what you make of it.";

	void Awake () {
		if (scoreTracker == null) {
			DontDestroyOnLoad (gameObject);
			Load ();
			scoreTracker = this;
		} else if(scoreTracker != this) {
			Destroy (gameObject);
		}
	}

	void OnLevelWasLoaded(){
		if (SceneManager.GetActiveScene ().name == "GrassChapter" ||
		    SceneManager.GetActiveScene ().name == "MistChapter") {
			currentScore = 0;
			currentScoreMultiplier = 1;
			active = true;
		}
	}
	// Use this for initialization
	void Start () {
		active = true;
		currentScore = 0;
		currentScoreMultiplier = 1;
	}

    // Update is called once per frame
    void FixedUpdate () {
		if (active) {
			currentScore += 1 * currentScoreMultiplier;
		}
	}

	void OnDestroy(){
        //Do not save on destroy. For some reason when OnDestroy is called
        //All variables already are written back to default values( 0 or false )
        //Save();
	}

	public void CatchCoin() {
		currentScore += 100 * currentScoreMultiplier;
	}

	public void CatchScoreMultiplier(){
		currentScoreMultiplier += 1;
	}

	public void Die (){
		active = false;
		LogScore ();
		Save();
	}

	void LogScore(){
		if (SceneManager.GetActiveScene ().name == "GrassChapter") {
			if (grassChapterHighScore < currentScore) {
				grassChapterHighScore = currentScore;
				if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
					SocialController.socialController.ReportScore("GrassChapter", grassChapterHighScore);
				}
			}
		} else if (SceneManager.GetActiveScene ().name == "MistChapter") {
			if (mistChapterHighScore < currentScore) {
				mistChapterHighScore = currentScore;
				if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
					SocialController.socialController.ReportScore("MistChapter", mistChapterHighScore);
				}
			}
		}
	}

	public void Save() {
		ScoreData data = new ScoreData(grassChapterHighScore, mistChapterHighScore);
		DataIO.Save<ScoreData>("ScoreSave", data);
		
	}

	public void Load() {
		ScoreData data = DataIO.Load<ScoreData>("ScoreSave");

        if(data != null)
        {
			grassChapterHighScore = data.grassChapterHighScore;
			mistChapterHighScore = data.mistChapterHighScore;
		}
	}
}


[Serializable]
class ScoreData {

	public long grassChapterHighScore = 0;
	public long mistChapterHighScore = 0;

	public ScoreData (long grass, long mist){
		grassChapterHighScore = grass;
		mistChapterHighScore = mist;
	}
}