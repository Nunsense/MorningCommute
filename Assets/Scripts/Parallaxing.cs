using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {
	public float limitX;
	public float offset;
	public Transform player;
	public Transform[] backgrounds;			// Array (list) of all the back- and foregrounds to be parallaxed
	private float[] parallaxScales;			// The proportion of the camera's movement to move the backgrounds by
	public float smoothing = 1f;			// How smooth the parallax is going to be. Make sure to set this above 0

	float prevPlayerPos = 0;
	public float distance;
	float[] positions;

	void Awake() {
		prevPlayerPos = player.position.x;

		parallaxScales = new float[backgrounds.Length];
		positions = new float[backgrounds.Length];
		for(int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z * smoothing;
			positions[i] = backgrounds[i].position.x;	
		}
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
				pos.x += distance * 2 + (distance - (playerX - pos.x ));
			}
			backgrounds[i].position = pos;
		}
	}
	
	public void Reset() {
		for(int i = 0; i < backgrounds.Length; i++) {
			Vector3 pos = backgrounds[i].position;
			pos.x = positions[i];
			backgrounds[i].position = pos;
		}
	}
}
