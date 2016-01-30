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
				Vector3 pos = oldLady.transform.localPosition;
				pos.x = 4.15f;
				pos.y = 1f;
				oldLady.transform.localPosition = pos;
				oldLady.gameObject.SetActive(true);
			} else {
				oldLady.gameObject.SetActive(false);
			}
		} else if (rand < 0.66f && elf) {
			if (rand < 0.55f) {
				Vector3 pos = elf.transform.localPosition;
				pos.x = 3.64f;
				pos.y = 1f;
				elf.transform.localPosition = pos;
				elf.gameObject.SetActive(true);
			} else {
				elf.gameObject.SetActive(false);
			}
		} else if (goblin) {
			if (rand < 0.88f) {
				Vector3 pos = goblin.transform.localPosition;
				pos.x = 3.64f;
				pos.y = 1f;
				goblin.transform.localPosition = pos;
				goblin.gameObject.SetActive(true);
			} else {
				goblin.gameObject.SetActive(false);
			}
		}
		
		if (oldLady) oldLady.Reset();
		if (goblin) goblin.Reset();
		if (elf) elf.Reset();
	}
}
