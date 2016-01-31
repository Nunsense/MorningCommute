using UnityEngine;
using System.Collections;

public class CoffeeBean : MonoBehaviour {
	public void Reset() {
		Vector3 rot = Random.rotationUniform.eulerAngles;
		rot.y = 0;
		rot.x = 0;
		transform.eulerAngles = rot;
	}
}
