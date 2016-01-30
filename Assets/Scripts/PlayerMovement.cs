using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float initialSpeed;
	[SerializeField] private float jumpForce;

	float speed;
	Rigidbody2D body;
	bool isGrounded;
	bool canDoubleJump;

	void Awake() {
		canDoubleJump = false;
		isGrounded = false;
		body = GetComponent<Rigidbody2D>();
	}

	void Start() {
		UpdateSpeed(initialSpeed);
	}

	void Update() {
		if (isGrounded) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				canDoubleJump = true;
				Jump();
			}	
		} else {
			if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump) {
				canDoubleJump = false;
				Jump();
			}
		}
	}

	void Jump() {
		body.AddForce(transform.up * jumpForce);
	}

	void FixedUpdate() {
		Vector2 pos = transform.position;
		pos.x += speed * Time.fixedDeltaTime;
		transform.position = pos;
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "ground") {
			isGrounded = false;
		}
	}

	void UpdateSpeed(float _speed) {
		speed = _speed;
	}
}
