using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class SimplesAds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if UNITY_IOS
			Advertisement.Initialize("1068856", false);
		#elif UNITY_ANDROID
			Advertisement.Initialize("1068857", false);
		#else
			print("Not built for a platform capable of ads.");
		#endif
	}
}
