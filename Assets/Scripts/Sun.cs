using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
	void Update () {
		Vector3 rot = transform.eulerAngles;
		rot.z += Time.deltaTime * 0.01f;
		transform.eulerAngles = rot;
	}
}
