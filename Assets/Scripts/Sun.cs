using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
	void Update () {
		Vector3 rot = transform.eulerAngles;
		rot.z -= Time.deltaTime;
		transform.eulerAngles = rot;
	}
}
