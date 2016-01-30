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
	}

	void Start() {
	
	}

	void Update() {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "coffeeBean") {
			coffeeLevel += 1;
			col.gameObject.SetActive(false);
			UpdateUI();

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
			case 4:
				Die();
				break;
			}
		} else if (col.tag == "goblin") {
			Die();
		} else if (col.tag == "oldLady" || col.tag == "elf") {
			if (coffeeLevel == 0) {
				Die();
			} else {
				coffeeLevel--;
				UpdateUI();
			}
		}
	}

	void Die() {
		gui.EndGame(movement.distance / 100);
	}

	void UpdateUI() {
		coffeLevelImage.UpdateLevels(coffeeLevel);
	}

	public void Reset() {
		movement.Reset();
		coffeeLevel = 1;
		UpdateUI();
	}
}
