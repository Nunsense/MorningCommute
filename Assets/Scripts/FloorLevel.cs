using UnityEngine;
using System.Collections;

public class FloorLevel : MonoBehaviour {

	CoffeeBean[] coffeeBeans;

	void Awake() {
		coffeeBeans = GetComponentsInChildren<CoffeeBean>();
	}

	public void Reset() {
		for (int i = 0; i < coffeeBeans.Length; i++) {
			coffeeBeans[i].gameObject.SetActive(true);
		}
	}
}
