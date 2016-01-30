using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoffeLevelImage : MonoBehaviour {

	public Sprite slowSprite;
	public Sprite normalSprite;
	public Sprite fastSprite;
	public Sprite superFastSprite;

	Image image;
	int level;

	void Start() {
		image = GetComponent<Image>();
	}

	void Update() {
//		if (level == 4) {
//			Quaternion rot = transform.rotation;
//			rot.eulerAngles *= Random.
//		}
	}

	public void UpdateLevels(int _level) {
		level = _level;
		switch (level) {
		case 0:
			image.sprite = slowSprite;
			break;
		case 1:
			image.sprite = normalSprite;
			break;
		case 2:
			image.sprite = fastSprite;
			break;
		case 3:
			image.sprite = superFastSprite;
			break;
		}
	}
}
