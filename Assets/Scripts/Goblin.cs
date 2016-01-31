using UnityEngine;
using System.Collections;

public class Goblin : MonoBehaviour {
	float speed = -0.7f;
	float patrolDistance = -5;
	float originX = 0;
	float targetX = 0;
	Animator anim;
	Rigidbody2D body;
	BoxCollider2D coll;

	void Awake() {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
	}
	
	void Start() {
		anim = GetComponent<Animator>();
	
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
	
	public void Reset() {
		coll.isTrigger = false;
		body.isKinematic = false;
	}
	
	public void TransformUp() {
		anim.SetTrigger("up");
	}
	
	public void TransformDown() {
		anim.SetTrigger("down");
	}
}
