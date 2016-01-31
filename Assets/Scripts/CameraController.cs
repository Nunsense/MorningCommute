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
	
	float bloomTime = 0.2f;
	float bloomingeTimeElapsed;
	bool blooming = false;

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
		
		if(blooming) {
			bloomingeTimeElapsed += Time.deltaTime;
			if(bloomingeTimeElapsed >= bloomTime) {
				blooming = false;
				transform.localScale = Vector3.one;
			}
			
			bloom.bloomIntensity = Mathf.Lerp(bloom.bloomIntensity, 1.5f, bloomingeTimeElapsed / bloomTime);
		}
	}
	
	public void Reset() {
		transform.position = origin;
	}
	
	public void SetMaxBlur() {
		bloom.bloomIntensity = 0;
		bloom.enabled = true;
		blooming = true;
		blur.blurAmount = 0.6f;
		blur.enabled = true;
	}
	
	public void SetMinBlur() {
		bloom.enabled = false;
		blur.blurAmount = 0.3f;
		blur.enabled = true;
	}
	
	public void SetNoneBlur() {
		blur.enabled = false;
		bloom.enabled = false;
	}
}
