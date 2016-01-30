using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float initialSpeed;
	[SerializeField] private float jumpForce;

	[SerializeField] float slowSpeed;
	[SerializeField] float normalSpeed;
	[SerializeField] float fastSpeed;
	[SerializeField] float superFastSpeed;

	public int distance;

	float currentSpeed;

	Rigidbody2D body;
	bool isGrounded;
	bool canDoubleJump;

	Vector2 origin;

	void Awake() {
		origin = transform.position;
		canDoubleJump = false;
		isGrounded = false;
		body = GetComponent<Rigidbody2D>();
	}

	void Start() {
		UpdateSpeed(initialSpeed);

		slowSpeed = initialSpeed / 2;
		normalSpeed = initialSpeed;
		fastSpeed = initialSpeed * 2;
		superFastSpeed = initialSpeed * 3;
		currentSpeed = normalSpeed;
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
		pos.x += currentSpeed * Time.fixedDeltaTime;
		distance += (int)currentSpeed;
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
		normalSpeed = _speed;
	}

	public void SetSlowSpeed() {
		currentSpeed = slowSpeed;
	}

	public void SetNormalSpeed() {
		currentSpeed = normalSpeed;
	}

	public void SetFastSpeed() {
		currentSpeed = fastSpeed;
	}

	public void SetSuperFastSpeed() {
		currentSpeed = superFastSpeed;
	}

	public void Reset() {
		transform.position = origin;
		distance = 0;
	}
}
