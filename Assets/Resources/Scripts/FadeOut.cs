using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {
	SpriteRenderer background;
	public float fadeSpeed = 3.14f;

	bool fade = false;
	float resetCountdown = 0f;

	void Start() {
		background = transform.parent.GetComponent<SpriteRenderer> ();
		background.color = new Color(1f ,1f ,1f ,1f);
	}


	void Update() {
		if (fade) {
			background.color = new Color(1,1,1, Mathf.Lerp(background.color.a, 0, (fadeSpeed*Time.deltaTime)));
			resetCountdown -= Time.deltaTime;

			if (resetCountdown <= 0) {
				print ("Resetting background alpha");
				fade = false;
				background.color = new Color (1, 1, 1, 1);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		print ("Something is hitting the trigger");
		if (other.name == "Player") {
			//print ("The Player is hitting the trigger");
			fade = true;
			resetCountdown = 13f;
			GameObject.Find ("EnvironmentController").GetComponent<ObstacleManager> ().currentScene++;
			GameObject.Find ("Background").GetComponent<BackgroundManager> ().ShiftBackground ();
			print ("Seconds since level load: " + Time.timeSinceLevelLoad.ToString());
		}
	}
}
