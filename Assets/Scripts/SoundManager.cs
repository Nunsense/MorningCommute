using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip[] oldLady;
	public AudioClip[] goblin;
	public AudioClip bird;
	public AudioClip[] coffee;
	public AudioClip[] pigeon;
	public AudioClip punch;
	public AudioClip ring;
	public AudioClip switchUp;
	public AudioClip switchDown;
	public AudioClip slow;
	public AudioClip normal;
	public AudioClip fast;
	public AudioClip superFast;
	public AudioSource soundfx;
	public AudioSource gameMusicSource;
	public AudioSource transitionsFxSource;
	public AudioSource menuMusicSource;
	private int currentGameMusicNumber;
	public static SoundManager instance = null;

	void Awake() {
		if(instance == null)
			instance = this;
		else
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Setup() {
		gameMusicSource.clip = normal;
		currentGameMusicNumber = 2;
	}

	public void playCoffee() {
		soundfx.clip = coffee[Random.Range(0, coffee.Length)];
		soundfx.Play();
	}

	public void playButton() {
		soundfx.clip = ring;
		soundfx.Play();
	}
	
	public void playPunch() {
		soundfx.clip = punch;
		soundfx.Play();
	}
	
	public void playGoblin() {
		soundfx.clip = goblin[Random.Range(0, goblin.Length)];
		soundfx.Play();
	}

	public void playOldLady() {
		soundfx.clip = oldLady[Random.Range(0, oldLady.Length)];
		soundfx.Play();
	}
	
	public void playPigeon() {
		soundfx.clip = pigeon[Random.Range(0, pigeon.Length)];
		soundfx.Play();
	}

	public void playMusicSlow() {
		gameMusicSource.Stop();
		transitionsFxSource.clip = switchDown;
		gameMusicSource.clip = slow;
		currentGameMusicNumber = 1;
		StartCoroutine(transition());
	}

	public void playMusicNormal() {
		gameMusicSource.Stop();

		if(currentGameMusicNumber > 2)
			transitionsFxSource.clip = switchDown;
		else
			transitionsFxSource.clip = switchUp;
		gameMusicSource.clip = normal;
		currentGameMusicNumber = 2;
		StartCoroutine(transition());
	}

	public void playMusicFast() {
		gameMusicSource.Stop();

		if(currentGameMusicNumber > 3)
			transitionsFxSource.clip = switchDown;
		else
			transitionsFxSource.clip = switchUp;
		gameMusicSource.clip = fast;
		currentGameMusicNumber = 3;
		StartCoroutine(transition());

	}

	public void playMusicSuperFast() {
		gameMusicSource.Stop();
		transitionsFxSource.clip = switchUp;
		gameMusicSource.clip = superFast;
		currentGameMusicNumber = 4;
		StartCoroutine(transition());
	}

	public void playMenuMusic() {
		gameMusicSource.Pause();
		menuMusicSource.Play();
	}

	public void stopMenuMusic() {
		menuMusicSource.Stop();
		gameMusicSource.Play();
	}
	
	public void StopGameMusic() {
		gameMusicSource.Stop();
	}

	private IEnumerator transition() {
		transitionsFxSource.Play();
		yield return new WaitWhile(() => transitionsFxSource.isPlaying);
		gameMusicSource.Play();
	}
}
