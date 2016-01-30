using UnityEngine;
using System.Collections;

public class FloorLevel : MonoBehaviour {
	CoffeeBean[] coffeeBeans;
	OldLady oldLady;
	Elf elf;
	Goblin goblin;

	void Awake() {
		coffeeBeans = GetComponentsInChildren<CoffeeBean>();
		oldLady = gameObject.GetComponentInChildren<OldLady>();
		elf = gameObject.GetComponentInChildren<Elf>();
		goblin = gameObject.GetComponentInChildren<Goblin>();
	}

	void Start() {
		HideEnemies();
	}

	void HideEnemies() {
		if (oldLady)
			oldLady.gameObject.SetActive(false);
		if (elf)
			elf.gameObject.SetActive(false);
		if (goblin)
			goblin.gameObject.SetActive(false);
	}

	public void Reset() {
		for (int i = 0; i < coffeeBeans.Length; i++) {
			coffeeBeans[i].gameObject.SetActive(true);
		}

		HideEnemies();
		float rand = Random.value;
		if (rand < 0.33f && oldLady) {
			if (rand < 0.1f) {
				oldLady.transform.localPosition = new Vector2(4.15f, 1f);
				oldLady.gameObject.SetActive(true);
			} else {
				oldLady.gameObject.SetActive(false);
			}
		} else if (rand < 0.66f && elf) {
			if (rand < 0.55f) {
				elf.transform.localPosition = new Vector2(3.64f, 1f);
				elf.gameObject.SetActive(true);
			} else {
				elf.gameObject.SetActive(false);
			}
		} else if (goblin) {
			if (rand < 0.88f) {
				goblin.transform.localPosition = new Vector2(3.64f, 1f);
				goblin.gameObject.SetActive(true);
			} else {
				goblin.gameObject.SetActive(false);
			}
		}
	}
}
