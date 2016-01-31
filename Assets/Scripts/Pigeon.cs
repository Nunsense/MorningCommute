using UnityEngine;
using System.Collections;

public class Pigeon : MonoBehaviour {
	float speed = 1.7f;
	float patrolDistance = 4;
	float originY = 0;
	float targetY = 0;
	Animator anim;
	Rigidbody2D body;
	BoxCollider2D coll;
	Vector3 origin;
	
	void Awake() {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
	}
	
	void Start() {
		anim = GetComponent<Animator>();
		
		origin = transform.localPosition;
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
	
	public void Reset() {
		coll.isTrigger = false;
		body.isKinematic = false;
		transform.localPosition = origin;
	}
	
	public void TransformUp() {
		anim.SetTrigger("up");
	}
	
	public void TransformDown() {
		anim.ResetTrigger("up");
		anim.SetTrigger("down");
	}
	
}
