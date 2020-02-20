using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	//This should always be true, but maybe not. Casper the friendly ghost later?
	public bool deadly = true;

	//Movement will be positive forward and negative backward(towards the character)
	public float movementSpeed = -2f;

	//Flag to determine if this object has any vertical movement
	public bool sinMovement = false;
	//This is the rate at which the sin curve turns, or the coefficient of the angle inside the sin function
	public float sinRotationSpeed = 3f;
	//This is the block units above the objects initial point that it will rise at top most point of sin curve
	public float sinRotationIntensity = 3f;


	//Handles projectiles
	public bool firesProjectiles = false;
	public float fireRate = 2.0f;
	public float fireAngle = 180f;
	public GameObject projectile;


	Animator anim;
	Collider2D collider;

	void Start(){
		anim = GetComponent<Animator> ();
	}

	void Update(){
		transform.Translate (movementSpeed * Time.deltaTime, VerticalMovement() * Time.deltaTime, 0f);
	}

	void Move(){
		
	}

	float VerticalMovement(){
		if (sinMovement) {
			return Mathf.Sin (Time.timeSinceLevelLoad * sinRotationSpeed) * sinRotationIntensity;
		} else {
			return 0f;
		}
	}
}
