using UnityEngine;
using System.Collections;

public class FloorController : MonoBehaviour {

	FloorLevel[] levels;

	FloorLevel currentLevel;

	void Awake() {
		levels = GetComponentsInChildren<FloorLevel>();
	}

	void Start() {
		for (int i = 0; i < levels.Length; i++) {
			levels[i].gameObject.SetActive(false);
		}
		Reset();
	}

	public void Reset() {
		if (currentLevel)
			currentLevel.gameObject.SetActive(false);
		
		currentLevel = levels[Random.Range(0, levels.Length)];
		currentLevel.gameObject.SetActive(true);
		currentLevel.Reset();
	}
}
