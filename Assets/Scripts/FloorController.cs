﻿using UnityEngine;
using System.Collections;

public class FloorController : MonoBehaviour {

	Vector2 origin;
	FloorLevel[] levels;
	FloorLevel currentLevel;

	void Awake() {
		origin = transform.position;
		levels = GetComponentsInChildren<FloorLevel>();
	}

	void Start() {
		for (int i = 0; i < levels.Length; i++) {
			levels[i].gameObject.SetActive(false);
		}
		ResetContent();
	}

	public void Reset() {
		transform.position = origin;
		ResetContent();
	}

	public void ResetContent() {
		if (currentLevel)
			currentLevel.gameObject.SetActive(false);
		
		currentLevel = levels[Random.Range(0, levels.Length)];
		currentLevel.gameObject.SetActive(true);
		currentLevel.Reset();
	}
}
