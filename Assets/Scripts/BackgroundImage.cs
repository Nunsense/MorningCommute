using UnityEngine;
using System.Collections;

public class BackgroundImage : MonoBehaviour {
	SpriteRenderer rend;
	
	public Sprite normal;
	public Sprite slow;
	
	void Awake() {
		rend = GetComponent<SpriteRenderer>();
	}
	
	public void SetNormal() {
		rend.sprite = normal;
	}
	
	public void SetSlow() {
		rend.sprite = slow;
	}
}
