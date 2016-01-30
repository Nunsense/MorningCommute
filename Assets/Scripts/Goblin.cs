using UnityEngine;
using System.Collections;

public class Goblin : MonoBehaviour {
	float speed = 0.7f;
	float patrolDistance = 5;
	float originX = 0;
	float targetX = 0;

	void Start() {
		originX = transform.position.x;
		targetX = originX + patrolDistance;
	}

	void FixedUpdate() {
		Vector2 pos = transform.position;
		if (speed > 0) {
			if (pos.x < targetX) {
				pos.x += speed * Time.fixedDeltaTime;
				transform.position = pos;
			} else {
				Turn();
			}
		} else {
			if (pos.x > targetX) {
				pos.x += speed * Time.fixedDeltaTime;
				transform.position = pos;
			} else {
				Turn();
			}
		}
	}

	void Turn() {
		float temp = targetX;
		targetX = originX;
		originX = temp;
		speed = -speed;
	}
}
