using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
	public Transform player;
	public PlayerMovement movement;
	public CameraController cameraController;
	public TopDistanceFlag flag;
	FloorController[] floors;
	[SerializeField]
	float
		floorWidth;
	float lastFloorX;
	int lastFloorIndex = 0;
	int topDistance;

	void Awake() {
		movement = player.GetComponent<PlayerMovement>();
	}
	
	void Start() {
		floors = new FloorController[4];
		floors[0] = GetComponentInChildren<FloorController>();
		floors[0].Initialize();
		GameObject floor = floors[0].gameObject;
		
		GameObject floor1 = GameObject.Instantiate(floor);
		floor1.transform.parent = transform;
		floor1.transform.position = new Vector2(floorWidth, 0);
		floors[1] = floor1.GetComponent<FloorController>();
		floors[1].Initialize();
		
		GameObject floor2 = Instantiate(floor);
		floor2.transform.parent = transform;
		floor2.transform.position = new Vector2(floorWidth * 2, 0);
		floors[2] = floor2.GetComponent<FloorController>();
		floors[2].Initialize();
		
		GameObject floor3 = Instantiate(floor);
		floor3.transform.parent = transform;
		floor3.transform.position = new Vector2(floorWidth * 3, 0);
		floors[3] = floor3.GetComponent<FloorController>();
		floors[3].Initialize();
		Reset();
	}

	void Update() {
		if(player.position.x >= lastFloorX) {
			FloorController floor = floors[lastFloorIndex];
			lastFloorIndex = (lastFloorIndex + 1) % floors.Length;

			lastFloorX += floorWidth;
			Vector3 pos = floor.transform.position;
			pos.x = lastFloorX + floorWidth;
			pos.y = 0;
			floor.transform.position = pos;
			floor.ResetContent(movement.distance);
		}
	}

	public void EndGame(int distance) {
		SetTopDistnce(distance);
	}

	public void Reset() {
		for(int i = 0; i < floors.Length; i++) {
			floors[i].Reset();
		}
		lastFloorIndex = 0;
		player.GetComponent<PlayerController>().Reset();
		lastFloorX = floorWidth * 2;
		cameraController.Reset();
		
		int top = GetTopDistnce();
		if(top > 0) {
			Vector3 pos = flag.transform.position;
			pos.x = top;
			flag.transform.position = pos;
			flag.gameObject.SetActive(true);
		} else {
			flag.gameObject.SetActive(false);
		}
	}
	
	public int GetTopDistnce() {
		return PlayerPrefs.GetInt("top_distance");
	}
	
	public void SetTopDistnce(int dist) {
		if(GetTopDistnce() < dist)
			PlayerPrefs.SetInt("top_distance", dist);
	}
}
