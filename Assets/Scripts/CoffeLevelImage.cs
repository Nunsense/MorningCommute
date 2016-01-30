using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoffeLevelImage : MonoBehaviour {

	public Sprite level1Sprite;
	public Sprite level2Sprite;
	public Sprite level3Sprite;

	Image image;

	void Start() {
		image = GetComponent<Image>();
	}

	void Update() {
	
	}

	public void UpdateLevels(float level) {
		if (level <= 10) {
			image.sprite = level1Sprite;
		} else if (level <= 20) {
			image.sprite = level2Sprite;
		} else {
			image.sprite = level3Sprite;
		}				
	}
}
