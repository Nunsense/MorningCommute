using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {
	public float limitX;
	public float offset;
	public Transform player;
	public Transform[] backgrounds;			// Array (list) of all the back- and foregrounds to be parallaxed
	private float[] parallaxScales;			// The proportion of the camera's movement to move the backgrounds by
	public float smoothing = 1f;			// How smooth the parallax is going to be. Make sure to set this above 0

	BackgroundImage[] images;
	float prevPlayerPos = 0;
	public float distance;
	float[] positions;
	float pulseTime = 0.2f;
	float pulseTimeElapsed;
	bool pulsing = false;
	
	Color slowBg;
	Color normalBg;

	void Awake() {
		prevPlayerPos = player.position.x;

		parallaxScales = new float[backgrounds.Length];
		positions = new float[backgrounds.Length];
		images = new BackgroundImage[backgrounds.Length];
		for(int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z * smoothing;
			positions[i] = backgrounds[i].position.x;	
			images[i] = backgrounds[i].GetComponent<BackgroundImage>();
		}
		
		slowBg = new Color(.91f, .47f, .51f, 1);
		normalBg = new Color(.78f, .91f, .71f, 1);
	}
	
	void Update() {
		Vector3 pos;
		float playerX = player.position.x;
		float diff = playerX - prevPlayerPos;
		prevPlayerPos = playerX;
		for(int i = 0; i < backgrounds.Length; i++) {
			pos = backgrounds[i].position;
			pos.x -= diff * parallaxScales[i];

			if(playerX - pos.x > distance) {
				pos.x += distance * 2;
			}
			backgrounds[i].position = pos;
		}
		
		if(pulsing) {
			pulseTimeElapsed += Time.deltaTime;
			if(pulseTimeElapsed >= pulseTime) {
				pulsing = false;
				transform.localScale = Vector3.one;
			}
				
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, pulseTimeElapsed / pulseTime);
		}
	}
	
	public void Reset() {
		for(int i = 0; i < backgrounds.Length; i++) {
			Vector3 pos = backgrounds[i].position;
			pos.x = positions[i];
			backgrounds[i].position = pos;
		}
		SetNormal();
	}
	
	public void SetSlow() {
		Pulse();
		for(int i = 0; i < images.Length; i++) {
			images[i].SetSlow();
		}
		Camera.main.backgroundColor = slowBg;
	}
	
	public void SetNormal() {
		Pulse();
		for(int i = 0; i < images.Length; i++) {
			images[i].SetNormal();
		}
		Camera.main.backgroundColor = normalBg;
	}
	
	void Pulse() {
		pulseTimeElapsed = 0;
		pulsing = true;
		transform.localScale = transform.localScale * 1.2f;
	}
}
