using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoffeLevelImage : MonoBehaviour {

	Animator anim;
	public float shakeAmountCrazy = 0.7f;
	public float shakeAmount = 0.7f;
	public Transform crazyImage;
	public Sprite slowSprite;
	public Sprite normalSprite;
	public Sprite fastSprite;
	public Sprite superFastSprite;
	Vector3 crazyImageOrigin;
	Vector3 origin;
	Image image;
	int level;

	void Awake() {
		anim = GetComponent<Animator>();
		image = GetComponent<Image>();
	}
	
	void Start() {
		crazyImageOrigin = crazyImage.localPosition;
		origin = transform.localPosition;
	}

	void Update() {
		if(level == 2) {
			Shake(shakeAmount);
		} else if(level == 3) {
			Shake(shakeAmountCrazy);
		}
	}
	
	void Shake(float amount) {
		crazyImage.localPosition = crazyImageOrigin + Random.insideUnitSphere * amount;
		transform.localPosition = origin - Random.insideUnitSphere * amount;
	}

	public void UpdateLevels(int _level) {
		anim.SetTrigger("action");
		level = _level;
		switch (level) {
		case 0:
			image.sprite = slowSprite;
			crazyImage.gameObject.SetActive(false);
			break;
		case 1:
			image.sprite = normalSprite;
			crazyImage.gameObject.SetActive(false);
			break;
		case 2:
			image.sprite = fastSprite;
			crazyImage.gameObject.SetActive(true);
			break;
		case 3:
			image.sprite = superFastSprite;
			crazyImage.gameObject.SetActive(true);
			break;
		}
	}
	
	public void Reset() {
		crazyImage.localPosition = crazyImageOrigin;
		transform.localPosition = origin;
	}
}
