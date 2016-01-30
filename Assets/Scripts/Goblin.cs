using UnityEngine;
using System.Collections;

public class Goblin : MonoBehaviour {
	float speed = 0.7f;
	float patrolDistance = 5;
	float originX = 0;
	float targetX = 0;
	
	Rigidbody2D body;
	BoxCollider2D coll;

	void Awake() {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
	}

	void Start() {
		originX = transform.position.x;
		targetX = originX + patrolDistance;
	}

	void FixedUpdate() {
		Vector3 pos = transform.position;
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
	
	public void Reset() {
		coll.isTrigger = false;
		body.isKinematic = false;
	}
}
