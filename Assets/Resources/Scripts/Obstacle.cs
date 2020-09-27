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
	public float fireSpeed = 10.0f;
	public float projectileLifespan = 3.0f;
	private float timeSinceLastFire = 0.0f;
	public GameObject projectile;


	Animator anim;
	Collider2D collider;

	void Start(){
		anim = GetComponent<Animator> ();
	}

	void Update(){
		transform.Translate (movementSpeed * Time.deltaTime, VerticalMovement() * Time.deltaTime, 0f);

        /*if (firesProjectiles)
        {
			if (timeSinceLastFire > fireRate)
			{
				FireProjectile();
			}
			else
            {
				timeSinceLastFire += Time.deltaTime;
            }
		}*/
	}

	float VerticalMovement(){
		if (sinMovement) {
			return Mathf.Sin (Time.timeSinceLevelLoad * sinRotationSpeed) * sinRotationIntensity;
		} else {
			return 0f;
		}
	}

	void FireProjectile()
    {
		GameObject firedProjectile = GameObject.Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0,0,fireAngle));
		Rigidbody2D rigidBody = firedProjectile.AddComponent<Rigidbody2D>();
		rigidBody.AddForce(rigidBody.transform.forward * fireSpeed);
		Destroy(firedProjectile, projectileLifespan);
		
		timeSinceLastFire = 0.0f;
    }
}
