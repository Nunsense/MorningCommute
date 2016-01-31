using UnityEngine;
using System.Collections;

public class OldLady : MonoBehaviour {
	float speed = 0.1f;
	Rigidbody2D body;
	BoxCollider2D coll;
	Animator anim;
	Vector3 origin;
	
	void Awake() {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
	}

	void Start() {
		origin = transform.localPosition;
	}

	void FixedUpdate() {
		Vector3 pos = transform.position;
		pos.x += speed * Time.fixedDeltaTime;
		transform.position = pos;
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
