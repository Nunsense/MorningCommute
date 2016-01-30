﻿using UnityEngine;
using System.Collections;

public class Elf : MonoBehaviour {
	float speed = -0.3f;

	void FixedUpdate() {
		Vector3 pos = transform.position;
		pos.x += speed * Time.fixedDeltaTime;
		transform.position = pos;
	}
}
