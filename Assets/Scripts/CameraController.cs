using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour {
	public Transform target;
	private Vector3 posOffset;
	private Vector3 posTemp;
	private Vector3 destinationPos;
	Vector3 origin;
	float yScale = 0.09f;
	float xScale = 4.5f;
	MotionBlur blur;

	void Awake() {
		blur = GetComponent<MotionBlur>();
		origin = transform.position;
		posOffset = target.position - origin;
	}

	void Update() {
		posTemp = transform.position;
		destinationPos = target.position - posOffset;
		
		posTemp.x -= (posTemp.x - destinationPos.x) * xScale * Time.deltaTime;
		if(posTemp.x < -1.64f)
			posTemp.x = -1.64f;
		posTemp.z = -8;
	
		transform.position = posTemp;
	}
	
	public void Reset() {
		transform.position = origin;
	}
	
	public void SetMaxBlur() {
		blur.blurAmount = 0.5f;
	}
	
	public void SetMinBlur() {
		blur.blurAmount = 0.3f;
	}
	
	public void SetNoneBlur() {
		blur.blurAmount = 0;
	}
}
