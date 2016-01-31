using UnityEngine;
using System.Collections;

public class FloorController : MonoBehaviour {

	Vector3 origin;
	FloorLevel[] levels;
	FloorLevel currentLevel;

	void Awake() {
		levels = GetComponentsInChildren<FloorLevel>();
	}

	void Start() {
		for (int i = 0; i < levels.Length; i++) {
			levels[i].gameObject.SetActive(false);
		}
		ResetContent(0);
	}
	
	public void Initialize() {
		origin = transform.position;
	}

	public void Reset() {
		transform.position = origin;
		ResetContent(0);
	}

	public void ResetContent(float distance) {
		if (currentLevel)
			currentLevel.gameObject.SetActive(false);
		
		if (distance < 20) {
			currentLevel = levels[Random.Range(0, Mathf.CeilToInt(levels.Length / 3))];
		} else if (distance > 20 && distance < 50) {
			currentLevel = levels[Random.Range(0, levels.Length)];
		} else {
			currentLevel = levels[Random.Range(0, Mathf.CeilToInt(2 * (levels.Length / 3)))];
		}
		
		currentLevel.gameObject.SetActive(true);
		currentLevel.Reset();
	}
	
	public void TransformUp() {
		for (int i = 0; i < levels.Length; i++) {
			levels[i].TransformUp();
		}
	}
	
	public void TransformDown() {
		for (int i = 0; i < levels.Length; i++) {
			levels[i].TransformDown();
		}
	}
}

