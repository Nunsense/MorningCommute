using UnityEngine;
using System.Collections;

public class FloorLevel : MonoBehaviour {
	CoffeeBean[] coffeeBeans;
	OldLady oldLady;
	Pigeon elf;
	Goblin goblin;

	void Awake() {
		coffeeBeans = GetComponentsInChildren<CoffeeBean>();
		oldLady = gameObject.GetComponentInChildren<OldLady>();
		elf = gameObject.GetComponentInChildren<Pigeon>();
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
				oldLady.Reset();
				oldLady.gameObject.SetActive(true);
			} else {
				oldLady.gameObject.SetActive(false);
			}
		} else if (rand < 0.66f && elf) {
			if (rand < 0.55f) {
				elf.Reset();
				elf.gameObject.SetActive(true);
			} else {
				elf.gameObject.SetActive(false);
			}
		} else if (goblin) {
			if (rand < 0.88f) {
				goblin.Reset();
				goblin.gameObject.SetActive(true);
			} else {
				goblin.gameObject.SetActive(false);
			}
		}
		
		if (oldLady) oldLady.Reset();
		if (goblin) goblin.Reset();
		if (elf) elf.Reset();
	}	
	
	public void TransformUp() {
		if (oldLady && oldLady.gameObject.activeSelf) oldLady.TransformUp();
		if (goblin && goblin.gameObject.activeSelf) goblin.TransformUp();
		if (elf && elf.gameObject.activeSelf) elf.TransformUp();
	}
	
	public void TransformDown() {
		if (oldLady && oldLady.gameObject.activeSelf) oldLady.TransformDown();
		if (goblin && goblin.gameObject.activeSelf) goblin.TransformDown();
		if (elf && elf.gameObject.activeSelf) elf.TransformDown();
	}
}
