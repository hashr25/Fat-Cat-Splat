using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

#if (PLATFORM_IOS == true)
using UnityEngine.SocialPlatforms.GameCenter;
#endif

public class SocialController : MonoBehaviour {

#if (PLATFORM_IOS)
	private string grassChapterLeaderboard = "grp.GrassChapter";
	private string mistChapterLeaderboard = "grp.MistChapter";
	private string grassChapterPassedAchievement = "grp.GrassChapterPassed";
	private string mistChapterPassedAchievement = "grp.MistChapterCompleted";
#elif (PLATFORM_ANDROID)
	private string grassChapterLeaderboard = "CgkIjeH7pMgbEAIQAA";
	private string mistChapterLeaderboard = "CgkIjeH7pMgbEAIQAQ";
	private string grassChapterPassedAchievement = "CgkIjeH7pMgbEAIQAg";
	private string mistChapterPassedAchievement = "CgkIjeH7pMgbEAIQAw";
#endif

	public static SocialController socialController;

	void Awake () {
		if (socialController == null) {
			DontDestroyOnLoad (gameObject);
			socialController = this;
		} else if(socialController != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		// Authenticate and register a ProcessAuthentication callback
		// This call needs to be made before we can proceed to other calls in the Social API
		Social.localUser.Authenticate (ProcessAuthentication);
	}

	// This function gets called when Authenticate completes
	// Note that if the operation is successful, Social.localUser will contain data from the server. 
	void ProcessAuthentication (bool success) {
		if (success) {
			//Debug.Log ("Authenticated, checking achievements");

			// Request loaded achievements, and register a callback for processing them
			Social.LoadAchievements (ProcessLoadedAchievements);
		}
		else
			Debug.Log ("Failed to authenticate");
	}

	// This function gets called when the LoadAchievement call completes
	void ProcessLoadedAchievements (IAchievement[] achievements) {
		/*if (achievements.Length == 0)
			Debug.Log ("Error: no achievements found");
		else
			Debug.Log ("Got " + achievements.Length + " achievements");
		*/
		// You can also call into the functions like this
		Social.ReportProgress ("Achievement01", 100.0, result => {
			if (result)
				Debug.Log ("Successfully reported achievement progress");
			else
				Debug.Log ("Failed to report achievement");
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowLeaderboards(){
#if (PLATFORM_IOS == true)
		GameCenterPlatform.ShowLeaderboardUI (grassChapterLeaderboard, TimeScope.AllTime);
#elif (PLATFORM_ANDROID == true)
		Social.ShowLeaderboardUI();
#endif
	}

	public void ReportScore(string chapter, long score){
		if (chapter == "GrassChapter") {
			Social.ReportScore (score, grassChapterLeaderboard, DebugScoreResult);
		} else if (chapter == "MistChapter") {
			Social.ReportScore (score, mistChapterLeaderboard, DebugScoreResult);
		}
	}

	public void ReportAchievement(string achievement){
		if (achievement == "GrassChapter") {
			Social.ReportProgress (grassChapterPassedAchievement, 100.0, DebugAchievementResult);
		} else if (achievement == "MistChapter") {
			Social.ReportProgress(mistChapterPassedAchievement, 100.0, DebugAchievementResult);
		}
	}

	void DebugScoreResult(bool result){
		if (result) {
			print ("Score reported successfully.");
		} else {
			print ("Score was not reported.");
		}
	}

	void DebugAchievementResult(bool result){
		if (result) {
			print ("Achievement reported successfully.");
		} else {
			print ("Achievement was not reported.");
		}
	}
}
