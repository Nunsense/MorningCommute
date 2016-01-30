using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {

	public Canvas retry;

	void Start() {
		retry.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	public void Pause() {
		Time.timeScale = 0;
		retry.gameObject.SetActive(true);
	}

	public void Resume() {
		retry.gameObject.SetActive(false);
		Time.timeScale = 1;
	}
}
