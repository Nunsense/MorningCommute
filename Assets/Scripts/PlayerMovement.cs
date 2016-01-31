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
	bool canDoubleJump;
	Vector3 origin;
	PlayerController controller;

	void Awake() {
		controller = GetComponent<PlayerController>();
		origin = transform.position;
		canDoubleJump = false;
		isGrounded = false;
		isJumping = false;
		body = GetComponent<Rigidbody2D>();
	}

	void Start() {
		slowSpeed = normalSpeed / 2;
		fastSpeed = normalSpeed * 2;
		superFastSpeed = normalSpeed * 3;
		
		SetNoSpeed();
	}

	void Update() {
		if(isGrounded) {
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Fire1")) {
				controller.TriggerJump();
				canDoubleJump = true;
				isJumping = true;
				Jump();
			}	
		} else {
			if(controller.GetCoffee() > 0 && (Input.GetKeyDown(KeyCode.Space) ||  Input.GetButton("Fire1")) && canDoubleJump) {
				controller.TriggerJump();
				canDoubleJump = false;
				controller.ConsumeCoffee();
				DoubleJump();
			}
		}
		
		Vector3 pos = transform.position;
		float prevX = pos.x;
		pos.x += currentSpeed * Time.deltaTime;
		transform.position = pos;
	}

	void Jump() {
		body.AddForce(transform.up * jumpForce);
	}
	
	void DoubleJump() {
		body.AddForce(transform.up * (jumpForce / 2));
	}

	void OnCollisionStay2D(Collision2D col) {
		if(col.gameObject.tag == "ground") {
			isGrounded = true;
			isJumping = false;
			controller.TriggerGrounded();
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if(col.gameObject.tag == "ground") {
			isGrounded = false;
			if(!isJumping)
				controller.TriggerFall();
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
		body.Sleep();
		body.WakeUp();
	}
}
