using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	int coffeeLevel;

	PlayerMovement movement;

	public CoffeLevelImage coffeLevelImage;

	void Awake() {
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
			case 1:
				movement.SetSlowSpeed();
				break;
			case 2:
				movement.SetNormalSpeed();
				break;
			case 3:
				movement.SetFastSpeed();
				break;
			case 4:
				movement.SetSuperFastSpeed();
				break;
			}
		}
	}

	void UpdateUI() {
		coffeLevelImage.UpdateLevels(coffeeLevel);
	}
}
