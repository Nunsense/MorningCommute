using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	float coffeeLevel;

	void Start () {
	
	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "coffeeBean") {
			coffeeLevel += 10;
			col.gameObject.SetActive(false);
		}
	}
}
