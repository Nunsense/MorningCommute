using UnityEngine;
using System.Collections;

public class TopDistanceFlag : MonoBehaviour {

	TextMesh text;
	
	void Awake() {
		text = GetComponentInChildren<TextMesh>();
	}

	public void SetDistance(int dist) {
		text.text = dist + "";
	}
}
