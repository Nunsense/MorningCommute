using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

	FloorController[] floors;

	public Transform player;
	[SerializeField] float floorWidth;
	float lastFloorX;
	int lastFloorIndex = 0;

	void Awake() {
		lastFloorX = floorWidth * 2;
		floors = GetComponentsInChildren<FloorController>();
	}

	void Update() {
		if (player.position.x >= lastFloorX) {
			FloorController floor = floors[lastFloorIndex];
			lastFloorIndex = (lastFloorIndex + 1) % floors.Length;

			lastFloorX += floorWidth;
			Vector3 pos = floor.transform.position;
			pos.x = lastFloorX + floorWidth;
			pos.y = 0;
			floor.transform.position = pos;
			floor.ResetContent();
		}
	}

	public void Reset() {
		for (int i = 0; i < floors.Length; i++) {
			floors[i].Reset();
		}
		lastFloorIndex = 0;
		player.GetComponent<PlayerController>().Reset();
		lastFloorX = floorWidth * 2;
	}
}
