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
	Bloom bloom;
	
	float speedingTime = 1f;
	float speedingTimeElapsed;
	bool speeding = false;

	float maxBlur;
	float maxBloom;

	void Awake() {
		blur = GetComponent<MotionBlur>();
		bloom = GetComponent<Bloom>();
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
		
		if(speeding) {
			speedingTimeElapsed += Time.deltaTime;
			if(speedingTimeElapsed >= speedingTime) {
				speeding = false;
				transform.localScale = Vector3.one;
			}
			
			bloom.bloomIntensity = Mathf.Lerp(bloom.bloomIntensity, maxBloom, speedingTimeElapsed / speedingTime);
			blur.blurAmount = Mathf.Lerp(blur.blurAmount, maxBlur, speedingTimeElapsed / speedingTime);
		}
	}
	
	public void Reset() {
		transform.position = origin;
	}
	
	public void SetMaxBlur() {
		maxBloom = 1.2f;
		bloom.bloomIntensity = 0;
		bloom.enabled = true;

		maxBlur = 0.6f;
		blur.blurAmount = 0f;
		blur.enabled = true;

		speedingTimeElapsed = 0;
		speeding = true;
	}
	
	public void SetMinBlur() {
		maxBloom = 0.3f;
		bloom.bloomIntensity = 0;
		bloom.enabled = true;

		maxBlur = 0.4f;
		blur.blurAmount = 0f;
		blur.enabled = true;

		speedingTimeElapsed = 0;
		speeding = true;
	}
	
	public void SetNoneBlur() {
		blur.enabled = false;
		bloom.enabled = false;
	}
}
