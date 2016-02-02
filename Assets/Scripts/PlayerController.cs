using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GuiManager gui;
	public CoffeLevelImage coffeLevelImage;
	public CameraController cameraController;
	public WorldController world;
	int coffeeLevel;
	int prevCoffeeLevel;
	PlayerMovement movement;
	float coffeeTimeElapsed;
	float coffeeTime = 5; //5 seconds
	Animator anim;
	bool isDead;

	void Awake() {
		isDead = false;
		coffeeTimeElapsed = coffeeTime;
		anim = GetComponent<Animator>();
		prevCoffeeLevel = 1;
		coffeeLevel = 1;
		movement = GetComponent<PlayerMovement>();
	}
	
	void Update() {
		if(transform.position.y < -0.5f) {
			Die();
		}
		
		coffeeTimeElapsed -= Time.deltaTime;
		if(coffeeTimeElapsed <= 0) {
			ConsumeCoffee();
			coffeeTimeElapsed = coffeeTime;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "coffeeBean") {
			SoundManager.instance.playCoffee();
			coffeeLevel += 1;
			col.gameObject.SetActive(false);
			if(coffeeLevel == 4)
				Die();
			else
				UpdateSpeed();
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "goblin") {
			if(coffeeLevel == 3) {
				col.collider.isTrigger = true;
				col.rigidbody.isKinematic = true;
				SoundManager.instance.playPunch();
				col.rigidbody.AddForce((col.transform.position - transform.position) * Random.Range(400, 600) + col.transform.up * Random.Range(4000, 6000));
			} else {
				SoundManager.instance.playGoblin();
				Die();
			}
		} else if(col.collider.tag == "oldLady") {
			if(coffeeLevel == 0) {
				SoundManager.instance.playOldLady();
				Die();
			} else if(coffeeLevel == 3) {
				col.collider.isTrigger = true;
				SoundManager.instance.playPunch();
				col.rigidbody.AddForce((col.transform.position - transform.position) * Random.Range(400, 600) + col.transform.up * Random.Range(400, 600));
			} else {
				SoundManager.instance.playOldLady();
				col.collider.isTrigger = true;
				col.rigidbody.isKinematic = true;
				coffeeLevel--;
				UpdateSpeed();
				UpdateUI();
			}
		} else if(col.collider.tag == "pigeon") {
			if(coffeeLevel == 0) {
				SoundManager.instance.playPigeon();
				Die();
			} else if(coffeeLevel == 3) {
				col.collider.isTrigger = true;
				SoundManager.instance.playPunch();
				col.rigidbody.AddForce((col.transform.position - transform.position) * Random.Range(400, 600) + col.transform.up * Random.Range(400, 600));
			} else {
				SoundManager.instance.playPigeon();
				col.collider.isTrigger = true;
				col.rigidbody.isKinematic = true;
				coffeeLevel--;
				UpdateSpeed();
				UpdateUI();
			}
		} else if(col.collider.tag == "ground") {
			if(movement.VerticalSpeed() > 0) {
				col.collider.isTrigger = true;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "ground") {
			col.isTrigger = false;
		}
	}
	
	void UpdateSpeed() {
		coffeeTimeElapsed = coffeeTime;
		anim.SetInteger("speed", coffeeLevel);
		switch (coffeeLevel) {
		case 0:
			world.TransformUp();
			movement.SetSlowSpeed();
			cameraController.SetNoneBlur();
			break;
		case 1:
			if(prevCoffeeLevel == 0)
				world.TransformDown();
			movement.SetNormalSpeed();
			cameraController.SetNoneBlur();
			break;
		case 2:
			movement.SetFastSpeed();
			cameraController.SetMinBlur();
			break;
		case 3:
			movement.SetSuperFastSpeed();
			cameraController.SetMaxBlur();
			break;
		}
		UpdateUI();
		prevCoffeeLevel = coffeeLevel;
	}
	
	void Die() {
		if(!isDead) {
			anim.SetTrigger("dead");
			isDead = true;
		}
	}
	
	void Dead() {
		gui.EndGame(transform.position.x);
	}

	void UpdateUI() {
		coffeLevelImage.UpdateLevels(coffeeLevel);
	}

	public void Reset() {
		isDead = false;
		prevCoffeeLevel = 1;
		coffeeLevel = 1;
		movement.Reset();
		movement.SetNoSpeed();
		UpdateUI();
		anim.SetBool("grounded", false);
		anim.SetBool("idDead", false);
		anim.ResetTrigger("jump");
		anim.ResetTrigger("dead");
		anim.ResetTrigger("fall");
		anim.SetTrigger("wake_up");
	}
	
	public void TriggerFall() {
		anim.SetTrigger("fall");
		anim.SetBool("grounded", false);
	}
	
	public void TriggerJump() {
		anim.SetTrigger("jump");
		anim.SetBool("grounded", false);
	}

	public void ResetJump() {
		anim.ResetTrigger("jump");
	}

	public void TriggerGrounded() {
		anim.SetBool("grounded", true);
	}

	public int GetCoffee() {
		return coffeeLevel;
	}

	public void ConsumeCoffee() {
		if(coffeeLevel > 0) {
			coffeeLevel--;
			UpdateSpeed();
			UpdateUI();
		}
	}
}
