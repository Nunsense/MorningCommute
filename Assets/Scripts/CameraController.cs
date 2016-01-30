using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform target;
	float yScale = 0.15f;
	float xScale = 4.5f;
	private Vector3 posOffset;
	private Vector3 posTemp;
	private Vector3 destinationPos;
	Vector3 origin;

	void Awake() {
		origin = transform.position;
		posOffset = target.position - origin;
	}

	void Update() {
		posTemp = transform.position;
		destinationPos = target.position - posOffset;

		posTemp.y -= (posTemp.y - destinationPos.y) * yScale * Time.deltaTime;
		posTemp.x -= (posTemp.x - destinationPos.x) * xScale * Time.deltaTime;
		posTemp.z = -8;

		transform.position = posTemp;
	}
	
	public void Reset() {
		transform.position = origin;
	}
}
