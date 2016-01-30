using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {
	public Canvas pause;
	public Canvas end;
	public Text finalDistance;
	public WorldController world;

	void Start() {
		pause.gameObject.SetActive(false);
		end.gameObject.SetActive(false);
	}

	void Update() {
	
	}

	public void Pause() {
		SoundManager.instance.playMenuMusic();
		Time.timeScale = 0;
		pause.gameObject.SetActive(true);
	}

	public void Resume() {
		SoundManager.instance.stopMenuMusic();
		pause.gameObject.SetActive(false);
		Time.timeScale = 1;
	}

	public void EndGame(float distance) {
		SoundManager.instance.playMenuMusic();
		Time.timeScale = 0;
		int dist = (int)distance;
		finalDistance.text = dist + "m";
		world.EndGame(dist);
		end.gameObject.SetActive(true);
	}

	public void Retry() {
		SoundManager.instance.stopMenuMusic();
		world.Reset();
		end.gameObject.SetActive(false);
		Time.timeScale = 1;
	}
}
