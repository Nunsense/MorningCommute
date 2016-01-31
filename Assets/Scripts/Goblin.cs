using UnityEngine;
using System.Collections;

public class Goblin : Enemy {
	float speed = -0.7f;
	public float patrolDistance = -5;
	float originX = 0;
	float targetX = 0;
	
	void Start() {
		originX = transform.position.x;
		targetX = originX + patrolDistance;
	}

	void FixedUpdate() {
		Vector3 pos = transform.position;
		if(speed > 0) {
			if(pos.x < targetX) {
				pos.x += speed * Time.fixedDeltaTime;
				transform.position = pos;
			} else {
				Turn();
			}
		} else {
			if(pos.x > targetX) {
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
		transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
	}
}
