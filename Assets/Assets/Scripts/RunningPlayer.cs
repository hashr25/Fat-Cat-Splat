using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (RunningController2D)) ]
public class RunningPlayer : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	public int maxNumberOfJumps = 2;
	public float moveSpeed = 6;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;
	int currentJumpNumber = 0;
	float currentMoveSpeed;

	float resetTimer = 3.0f;

	RunningController2D controller;
	Animator anim;

	[SerializeField] bool invincible = false;
	bool dead = false;

	void Start() {
		controller = GetComponent<RunningController2D> ();
		anim = GetComponent<Animator> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		currentMoveSpeed = moveSpeed;
		//print ("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);
	}

	void Update() {
		MovementAndCollisionDetection ();

		if (dead) {
			resetTimer -= Time.deltaTime;

            if (resetTimer <= 0)
            {
				GameObject.Find("In Game GUI").GetComponent<InGameUI>().PlayerDied();
				//SceneManager.LoadScene ("TitleScreen");
			}
		}
	}

	void MovementAndCollisionDetection () {
		//Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 input = new Vector2 (1,0);
		//int wallDirX = (controller.collisions.left) ? -1 : 1;
		//print(("HorizontalMovement: " + input.x.ToString()));
		float targetVelocityX = currentMoveSpeed;
		velocity.x = targetVelocityX;

		if (Input.GetKeyDown (KeyCode.Space) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began))  {
			/*if (wallSliding) {
				print ("trying to wall jump");
				if (wallDirX == input.x) {
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}
				else if (input.x == 0) {
					velocity.x = -wallDirX * wallJumpOff.x;
					velocity.y = wallJumpOff.y;
				}
				else {
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
				}
			}*/


			//On the ground Jumping
			if (controller.collisions.below && currentJumpNumber < 1) {
				velocity.y = maxJumpVelocity;
				currentJumpNumber = 1;
			} else if (currentJumpNumber < maxNumberOfJumps) { //In the air trying to jump
				velocity.y = maxJumpVelocity;
				currentJumpNumber++;
			}
		}

		/*if (Input.GetKeyUp (KeyCode.Space) || Input.touchCount == 0) {
			if (velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;

			}
		}*/


		velocity.y += gravity * Time.deltaTime;

		if (!dead) {
			controller.Move (velocity * Time.deltaTime, input);
		}

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
			anim.SetBool ("Grounded", true);

			if (controller.collisions.below) {
				currentJumpNumber = 0;
			}
		} else {
			anim.SetBool ("Grounded", false);
		}

		anim.SetFloat ("VerticalVelocity", velocity.y);

		if (controller.collisions.deadlyCollision) {
			Die ();
		}
	}

	public void Die() {
		if (!invincible) {
			dead = true;
			anim.SetBool ("Died", true);
			controller.enabled = false;
			currentMoveSpeed = 0f;
			GameController.gameController.StopBackground ();
			ScoreTracker.scoreTracker.Die ();
			GameController.gameController.Save();
			GameObject.Find("In Game GUI").GetComponent<InGameUI>().PlayerDied();
			//GameObject.Find ("GameController").GetComponent<AdManager> ().ShowAd ();
		}
	}

	public void Reset() {
		currentMoveSpeed = moveSpeed;
		transform.position = new Vector2 (0, -3.3f);
		invincible = false;
	}
}
