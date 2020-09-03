using UnityEngine;
using System.Collections;

// @NOTE the attached sprite's position should be "Top Right" or the children will not align properly
// Strech out the image as you need in the sprite render, the following script will auto-correct it when rendered in the game
[RequireComponent (typeof (SpriteRenderer))]

// Generates a nice set of repeated sprites inside a streched sprite renderer
// @NOTE Vertical only, you can easily expand this to horizontal with a little tweaking
public class RepeatSpriteBoundary : MonoBehaviour {
	public float gridX = 0.0f;
	public float gridY = 0.0f;

	SpriteRenderer sprite;

	void Awake () {

		sprite = GetComponent<SpriteRenderer>();
			
		if(!SpritePivotAlignment.GetSpriteAlignment(gameObject).Equals(SpriteAlignment.TopRight)){
			Debug.LogError("You forgot change the sprite pivot to Top Right.");
		}

		Vector2 spriteSize_wu = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);
		Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);

		if (0.0f != gridX) {
			float width_wu = sprite.bounds.size.x / gridX;
			scale.x = width_wu / spriteSize_wu.x;
			spriteSize_wu.x = width_wu;
		}

		if (0.0f != gridY) {
			float height_wu = sprite.bounds.size.y / gridY;
			scale.y = height_wu / spriteSize_wu.y;
			spriteSize_wu.y = height_wu;
		}

		GameObject childPrefab = new GameObject();

		SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
		childPrefab.transform.position = transform.position;
		childSprite.sprite = sprite.sprite;
		childSprite.sortingLayerID = SortingLayer.NameToID ("Background");
		childSprite.sortingOrder = 5;

		GameObject child;
		for (int i = 0, h = (int)Mathf.Round(sprite.bounds.size.y); i*spriteSize_wu.y < h; i++) {
			for (int j = 0, w = (int)Mathf.Round(sprite.bounds.size.x); j*spriteSize_wu.x < w; j++) {
				child = Instantiate(childPrefab) as GameObject;
				child.transform.position = transform.position - (new Vector3(spriteSize_wu.x*j, spriteSize_wu.y*i, 0));
				child.transform.localScale = scale;
				child.transform.parent = transform;
			}
		}

		Destroy(childPrefab);
		sprite.enabled = false; // Disable this SpriteRenderer and let the prefab children render themselves

	}
}