using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float normalSpeed;
	[SerializeField]
	private float jumpForce;
	float slowSpeed;
	float fastSpeed;
	float superFastSpeed;
	float currentSpeed;
	Rigidbody2D body;
	bool isGrounded;
	bool isJumping;
	bool isFalling;
	bool canDoubleJump;
	Vector3 origin;
	PlayerController controller;
	bool isMooving;

	float ChangeSpeedTime = 1f;
	float changeSpeedTimeElapsed;
	float maxSpeed;
	bool changeingSpeed = false;

	void Awake() {
		isMooving = true;
		isJumping = false;
		controller = GetComponent<PlayerController>();
		origin = transform.position;
		canDoubleJump = false;
		isGrounded = false;
		body = GetComponent<Rigidbody2D>();
	}

	void Start() {
		slowSpeed = normalSpeed / 2;
		fastSpeed = normalSpeed * 2;
		superFastSpeed = normalSpeed * 3;
		
		SetNoSpeed();
	}

	void Update() {
		if (isGrounded) {
			if (!isJumping && JumpInput()) {
				controller.TriggerJump();
				isMooving = false;
				isJumping = true;
				canDoubleJump = true;
			}	
		} else {
			if (!isFalling && body.velocity.y < 0) {
				controller.TriggerFall();
				isFalling = true; 
				canDoubleJump = true;
			}

			if (controller.GetCoffee() > 0 && JumpInput() && canDoubleJump) {
				controller.TriggerJump();
				canDoubleJump = false;
				controller.ConsumeCoffee();
				DoubleJump();
			}
		}

		if (isMooving) {
			if (changeingSpeed) {
				changeSpeedTimeElapsed += Time.deltaTime;
				if (changeSpeedTimeElapsed >= ChangeSpeedTime) {
					changeingSpeed = false;
				}

				currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, changeSpeedTimeElapsed / ChangeSpeedTime);
			}

			Vector3 pos = transform.position;
			float prevX = pos.x;
			pos.x += currentSpeed * Time.deltaTime;
			transform.position = pos;
		}
	}

	void Jump() {
		if (!isJumping)
			return;
		body.AddForce(transform.up * jumpForce);
		isMooving = true;
		isJumping = false;
		controller.ResetJump();
	}

	void DoubleJump() {
		isFalling = false;
		body.velocity = new Vector2(body.velocity.x, 0);
		body.AddForce(transform.up * jumpForce);
		canDoubleJump = false;
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "ground") {
			if (!isGrounded) {
				controller.TriggerGrounded();
				isGrounded = true;
				isFalling = false;
			}
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "ground") {
			if (isGrounded) {
				isGrounded = false;
			}
		}
	}

	public float VerticalSpeed() {
		return body.velocity.y;
	}

	public void SetNoSpeed() {
		maxSpeed = 0;
		changeingSpeed = true;
	}

	public void SetSlowSpeed() {
		SoundManager.instance.playMusicSlow();
		maxSpeed = slowSpeed;
		changeingSpeed = true;
	}

	public void SetNormalSpeed() {
		SoundManager.instance.playMusicNormal();
		maxSpeed = normalSpeed;
		changeingSpeed = true;
	}

	public void SetFastSpeed() {
		SoundManager.instance.playMusicFast();
		maxSpeed = fastSpeed;
		changeingSpeed = true;
	}

	public void SetSuperFastSpeed() {
		SoundManager.instance.playMusicSuperFast();
		maxSpeed = superFastSpeed;
		changeingSpeed = true;
	}

	public void Reset() {
		transform.position = origin;
		currentSpeed = normalSpeed;
		isJumping = false;
		isGrounded = true;
		isFalling = false;
		isMooving = true;
		body.velocity = Vector3.zero;	
		body.angularVelocity = 0; 
	}

	bool JumpInput() {
		return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
	}
}
