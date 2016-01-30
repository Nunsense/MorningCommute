using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float normalSpeed;
	[SerializeField] private float jumpForce;

	float slowSpeed;
	float fastSpeed;
	float superFastSpeed;

	public float distance;

	float currentSpeed;

	Rigidbody2D body;
	bool isGrounded;
	bool canDoubleJump;

	Vector3 origin;

	PlayerController controller;

	void Awake() {
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
		
		currentSpeed = normalSpeed;
	}

	void Update() {
		if (isGrounded) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				canDoubleJump = true;
				Jump();
			}	
		} else {
			if (controller.GetCoffee() > 0 && Input.GetKeyDown(KeyCode.Space) && canDoubleJump) {
				canDoubleJump = false;
				controller.ConsumeCoffee();
				Jump();
			}
		}
		
		Vector3 pos = transform.position;
		float prevX = pos.x;
		pos.x += currentSpeed * Time.deltaTime;
		distance += pos.x - prevX;
		transform.position = pos;
	}

	void Jump() {
		body.AddForce(transform.up * jumpForce);
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

	public float VerticalSpeed() {
		return body.velocity.y;
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
		currentSpeed = normalSpeed;
		body.Sleep();
		body.WakeUp();
	}
}
