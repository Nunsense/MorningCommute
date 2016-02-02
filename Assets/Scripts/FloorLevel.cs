using UnityEngine;
using System.Collections;

public class FloorLevel : MonoBehaviour {
	CoffeeBean[] coffeeBeans;
	OldLady oldLady;
	Pigeon elf;
	Goblin goblin;
	Enemy[] enemies;

	void Awake() {
		coffeeBeans = GetComponentsInChildren<CoffeeBean>();
		enemies = GetComponentsInChildren<Enemy>();
	}

	void Start() {
	}

	void HideEnemies() {
		for (int i = 0; i < enemies.Length; i++) {
			enemies[i].gameObject.SetActive(false);
		}
	}

	public void Reset() {
		for (int i = 0; i < coffeeBeans.Length; i++) {
			coffeeBeans[i].gameObject.SetActive(false);
		}
		
		int total = 0;
		int maxTries = coffeeBeans.Length;
		while (total < 2 && maxTries > 0) {
			int i = Random.Range(0, coffeeBeans.Length);
			if (!coffeeBeans[i].gameObject.activeSelf) {
				coffeeBeans[i].Reset();
				coffeeBeans[i].gameObject.SetActive(true);
				total++;
			}
			maxTries--;
		}

		if (enemies.Length > 0) {
			HideEnemies();
			float rand = Random.value;
			if (rand < .5f) { // one enemy
				int i = Random.Range(0, enemies.Length);
				Enemy enem = enemies[i];
				if (enem) {
					enemies[i].Reset();
					enemies[i].gameObject.SetActive(true);
				}
			} else { // two enemy
				total = 0;
				maxTries = enemies.Length;
				while (total < 2 && maxTries > 0) {
					int i = Random.Range(0, enemies.Length);
					if (!enemies[i].gameObject.activeSelf) {
						enemies[i].Reset();
						enemies[i].gameObject.SetActive(true);
						total++;
					}
				}
			}
		}
	}

	public void TransformUp() {
		for (int i = 0; i < enemies.Length; i++) {
			Enemy enem = enemies[i];
			if (enem && enem.gameObject.activeSelf)
				enem.TransformUp();
		}
	}

	public void TransformDown() {
		for (int i = 0; i < enemies.Length; i++) {
			Enemy enem = enemies[i];
			if (enem && enem.gameObject.activeSelf)
				enem.TransformDown();
		}
	}
}
