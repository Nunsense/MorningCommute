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
			if (!isJumping && Input.GetKeyDown(KeyCode.Space)) {
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

			if (controller.GetCoffee() > 0 && Input.GetKeyDown(KeyCode.Space) && canDoubleJump) {
				controller.TriggerJump();
				canDoubleJump = false;
				controller.ConsumeCoffee();
				DoubleJump();
			}
		}

		if (isMooving) {
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
		currentSpeed = 0;
	}

	public void SetSlowSpeed() {
		SoundManager.instance.playMusicSlow();
		currentSpeed = slowSpeed;
	}

	public void SetNormalSpeed() {
		SoundManager.instance.playMusicNormal();
		currentSpeed = normalSpeed;
	}

	public void SetFastSpeed() {
		SoundManager.instance.playMusicFast();
		currentSpeed = fastSpeed;
	}

	public void SetSuperFastSpeed() {
		SoundManager.instance.playMusicSuperFast();
		currentSpeed = superFastSpeed;
	}

	public void Reset() {
		transform.position = origin;
		currentSpeed = normalSpeed;
		body.velocity = Vector3.zero;
	}
}
