using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GuiManager gui;
	public CoffeLevelImage coffeLevelImage;
	int coffeeLevel;
	PlayerMovement movement;
	float coffeeTimeElapsed;
	float coffeeTime = 5; //5 seconds
	Animator anim;

	void Awake() {
		anim = GetComponent<Animator>();
		coffeeLevel = 1;
		movement = GetComponent<PlayerMovement>();
	}

	void Start() {
	
	}

	void Update() {
		if(transform.position.y < -0.5f) {
			Die();
		}
		
		coffeeTimeElapsed -= Time.deltaTime;
		if(coffeeTimeElapsed == 0) {
			ConsumeCoffee();
			coffeeTimeElapsed = coffeeTime;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "coffeeBean") {
			SoundManager.instance.playCoffee();
			coffeeLevel += 1;
			col.gameObject.SetActive(false);
			UpdateUI();

			if(coffeeLevel == 4)
				Die();
			else
				UpdateSpeed();	
//		} else if(col.tag == "goblin") {
//			if(coffeeLevel == 3) {
//				col.attachedRigidbody.AddForce((col.transform.position - transform.position) * 500);
//			} else {
//				Die();
//			}
//		} else if(col.tag == "oldLady" || col.tag == "elf") {
//			if(coffeeLevel == 0) {
//				Die();
//			} else if(coffeeLevel == 3) {
//				col.attachedRigidbody.AddForce((col.transform.position - transform.position) * 500);
//			} else {
//				coffeeLevel--;
//				UpdateUI();
//			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "goblin") {
			col.collider.isTrigger = true;
			if(coffeeLevel == 3) {
				SoundManager.instance.playPunch();
				col.rigidbody.AddForce((col.transform.position - transform.position) * Random.Range(400, 600) + col.transform.up * Random.Range(400, 600));
			} else {
				SoundManager.instance.playGoblin();
				Die();
			}
		} else if(col.collider.tag == "oldLady") {
			col.collider.isTrigger = true;
			if(coffeeLevel == 0) {
				SoundManager.instance.playOldLady();
				Die();
			} else if(coffeeLevel == 3) {
				SoundManager.instance.playPunch();
				col.rigidbody.AddForce((col.transform.position - transform.position) * Random.Range(400, 600) + col.transform.up * Random.Range(400, 600));
			} else {
				SoundManager.instance.playOldLady();
				col.rigidbody.isKinematic = true;
				coffeeLevel--;
				UpdateSpeed();
				UpdateUI();
			}
		} else if(col.collider.tag == "elf") {
			col.collider.isTrigger = true;
			if(coffeeLevel == 0) {
				SoundManager.instance.playOldLady();
				Die();
			} else if(coffeeLevel == 3) {
				SoundManager.instance.playPunch();
				col.rigidbody.AddForce((col.transform.position - transform.position) * Random.Range(400, 600) + col.transform.up * Random.Range(400, 600));
			} else {
				SoundManager.instance.playPigeon();
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
		switch (coffeeLevel) {
		case 0:
			movement.SetSlowSpeed();
			break;
		case 1:
			movement.SetNormalSpeed();
			break;
		case 2:
			movement.SetFastSpeed();
			break;
		case 3:
			movement.SetSuperFastSpeed();
			break;
		}
	}
	
	void Die() {
		gui.EndGame(movement.distance);
	}

	void UpdateUI() {
		coffeLevelImage.UpdateLevels(coffeeLevel);
	}

	public void Reset() {
		movement.Reset();
		coffeeLevel = 1;
		movement.SetNoSpeed();
		UpdateUI();
		anim.SetTrigger("wake_up");
	}

	public int GetCoffee() {
		return coffeeLevel;
	}

	public void ConsumeCoffee() {
		coffeeLevel--;
		UpdateUI();
	}
}
