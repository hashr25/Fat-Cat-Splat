using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierScript : MonoBehaviour
{
    float minSize = 1.25f;
    float maxSize = 1.5f;
    bool gettingBigger = true;

    private void Update()
    {
        if (gettingBigger)
        {
            float newScale = transform.localScale.x + Time.deltaTime;
            transform.localScale = new Vector3(newScale, newScale, newScale);

            if(newScale >= maxSize)
            {
                gettingBigger = false;
            }
        }
        else
        {
            float newScale = transform.localScale.x - Time.deltaTime;
            transform.localScale = new Vector3(newScale, newScale);

            if (newScale <= minSize)
            {
                gettingBigger = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		print("Someting is hitting the coin.");
		ScoreTracker.scoreTracker.CatchScoreMultiplier();
		InGameUI.inGameUI.UpdateScoreMultiplier();
		Destroy(gameObject);
	}
}
