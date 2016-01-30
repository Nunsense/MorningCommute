using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	int coffeeLevel;
	PlayerMovement movement;
	public GuiManager gui;
	public CoffeLevelImage coffeLevelImage;

	void Awake() {
		coffeeLevel = 1;
		movement = GetComponent<PlayerMovement>();
		UpdateSpeed();
	}

	void Start() {
	
	}

	void Update() {
		if(transform.position.y < -0.5f) {
			Die();
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "coffeeBean") {
			coffeeLevel += 1;
			col.gameObject.SetActive(false);
			UpdateUI();

			if(coffeeLevel == 4)
				Die();
			else
				UpdateSpeed();	
		} else if(col.tag == "goblin") {
			if(coffeeLevel == 3) {
				col.attachedRigidbody.AddForce((col.transform.position - transform.position) * 500);
			} else {
				Die();
			}
		} else if(col.tag == "oldLady" || col.tag == "elf") {
			if(coffeeLevel == 0) {
				Die();
			} else if(coffeeLevel == 3) {
				col.attachedRigidbody.AddForce((col.transform.position - transform.position) * 500);
			} else {
				coffeeLevel--;
				UpdateUI();
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "goblin") {
			col.collider.isTrigger = true;
			if(coffeeLevel == 3) {
				col.rigidbody.AddForce((col.transform.position - transform.position) * 200 + col.transform.up * Random.Range(200, 300));
			} else {
				Die();
			}
		} else if(col.collider.tag == "oldLady" || col.collider.tag == "elf") {
			col.collider.isTrigger = true;
			if(coffeeLevel == 0) {
				Die();
			} else if(coffeeLevel == 3) {
				col.rigidbody.AddForce((col.transform.position - transform.position) * 200 + col.transform.up * Random.Range(200, 300));
			} else {
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
		UpdateSpeed();
		UpdateUI();
	}

	public int GetCoffee() {
		return coffeeLevel;
	}

	public void ConsumeCoffee() {
		coffeeLevel--;
		UpdateUI();
	}
}
