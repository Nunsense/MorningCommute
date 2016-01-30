using UnityEngine;
using System.Collections;

public class OldLady : MonoBehaviour {
	float speed = 0.1f;

	void FixedUpdate() {
		Vector3 pos = transform.position;
		pos.x += speed * Time.fixedDeltaTime;
		transform.position = pos;
	}
}
