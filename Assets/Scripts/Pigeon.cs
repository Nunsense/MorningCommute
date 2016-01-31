using UnityEngine;
using System.Collections;

public class Pigeon : Enemy {
	float speed = 1.7f;
	public float patrolDistance = 4;
	float originY = 0;
	float targetY = 0;

	void Start() {
		originY = transform.position.y;
		targetY = originY + patrolDistance;
	}
	
	void FixedUpdate() {
		Vector3 pos = transform.position;
		if(speed > 0) {
			if(pos.y < targetY) {
				pos.y += speed * Time.fixedDeltaTime;
				transform.position = pos;
			} else {
				Turn();
			}
		} else {
			if(pos.y > targetY) {
				pos.y += speed * Time.fixedDeltaTime;
				transform.position = pos;
			} else {
				Turn();
			}
		}
	}
	
	void Turn() {
		float temp = targetY;
		targetY = originY;
		originY = temp;
		speed = -speed;
	}
}
