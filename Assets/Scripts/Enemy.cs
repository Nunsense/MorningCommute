using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	protected Animator anim;
	protected Rigidbody2D body;
	protected BoxCollider2D coll;
	protected Vector3 origin;

	void Awake() {
		body = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
		origin = transform.localPosition;
	}

	public void Reset() {
		transform.eulerAngles = Vector3.zero;
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
