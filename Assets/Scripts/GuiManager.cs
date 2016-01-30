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
		Time.timeScale = 0;
		pause.gameObject.SetActive(true);
	}

	public void Resume() {
		pause.gameObject.SetActive(false);
		Time.timeScale = 1;
	}

	public void EndGame(float distance) {
		Time.timeScale = 0;
		int dist = (int)distance;
		finalDistance.text = dist + "m";
		world.EndGame(dist);
		end.gameObject.SetActive(true);
	}

	public void Retry() {
		world.Reset();
		end.gameObject.SetActive(false);
		Time.timeScale = 1;
	}
}
