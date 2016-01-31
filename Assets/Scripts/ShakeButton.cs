using UnityEngine;
using System.Collections;

public class ShakeButton : MonoBehaviour {
	
	public float shakeAmount = 0.7f;
	Vector3 origin;
	
	void Start () {
		origin = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = origin - Random.insideUnitSphere * shakeAmount;
	}
}
