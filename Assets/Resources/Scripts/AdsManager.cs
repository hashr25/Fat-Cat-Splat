using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameId = "1068856";
    private bool isMobile = true;
#elif UNITY_ANDROID
    private string gameId = "1068857";
    private bool isMobile = true;
#else
    private string gameId = "NOTVALID";
    private bool isMobile = false;
#endif

#if DEBUG
    private bool testMode = true;
#else
    private bool testMode = false;
#endif

    private string rewardedPlacementId = "rewardedVideo";

    public static AdsManager adsManager;

    private int currentGoldReward = 100;
    private DateTime timeOfLastAdPlay;

    // Start is called before the first frame update
    void Start()
    {
        if (adsManager == null)
        {
            DontDestroyOnLoad(gameObject);
            adsManager = this;
        }
        else if (adsManager != this)
        {
            Destroy(gameObject);
        }

        if (isMobile)
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }
    }

    private void ShowSimpleAd()
    {
        if (isMobile) { Advertisement.Show(); }
        
    }

    private void ShowRewardedVideo()
    {
        if (isMobile) { Advertisement.Show(rewardedPlacementId); }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            GameController.gameController.coins += currentGoldReward;
            DialogSpawner.dialogSpawner.SpawnErrorDialog("You successfully watched the ad! You now have " + currentGoldReward.ToString() + " more coins!");

            //currentGoldReward = 0;
            timeOfLastAdPlay = DateTime.UtcNow;
        }
        else if(showResult == ShowResult.Skipped)
        {
            DialogSpawner.dialogSpawner.SpawnErrorDialog("You won't receive any coins! You skipped the ad!");
        }
        else if(showResult == ShowResult.Failed)
        {
            DialogSpawner.dialogSpawner.SpawnErrorDialog("There was some error while trying to watch your ad! Try again later!");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        //Log the error
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional acts to take when the end-user triggers an ad.
    }

    public void RunSimpleAd()
    {
        if (isMobile)
        {
            if (Advertisement.IsReady())
            {
                ShowSimpleAd();
            }
            else
            {
                DialogSpawner.dialogSpawner.SpawnErrorDialog("There are no ads available currently. Try again later!");
            }
        }
        
    }

    public void RunGoldRewardAd(int numOfCoins)
    {
        if (isMobile)
        {
            currentGoldReward = numOfCoins;
            if (Advertisement.IsReady(rewardedPlacementId))
            {
                ShowRewardedVideo();
            }
            else
            {
                DialogSpawner.dialogSpawner.SpawnErrorDialog("There are no ads available currently. Try again later!");
            }
        }
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        //Do something to show the ads are ready?
    }

    public bool IsRewardAdReady()
    {
        return (Advertisement.IsReady(rewardedPlacementId) &&(DateTime.UtcNow - timeOfLastAdPlay) > new TimeSpan(0, 5, 0));
    }
}
