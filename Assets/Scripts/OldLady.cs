using UnityEngine;
using System.Collections;

public class OldLady : MonoBehaviour {
	float speed = 0.1f;
	
	Rigidbody2D body;
	BoxCollider2D coll;
	
	void Awake() {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
	}

	void FixedUpdate() {
		Vector3 pos = transform.position;
		pos.x += speed * Time.fixedDeltaTime;
		transform.position = pos;
	}
	
	public void Reset() {
		coll.isTrigger = false;
		body.isKinematic = false;
	}
}
