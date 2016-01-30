using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

	FloorController[] floors;

	public Transform player;
	[SerializeField] float floorWidth;
	float lastFloorX;
	int lastFloorIndex = 0;

	void Awake () {
		lastFloorX = floorWidth * 2;
		floors = GetComponentsInChildren<FloorController>();
	}

	void Update () {
		if (player.position.x >= lastFloorX) {
			FloorController floor = floors[lastFloorIndex];
			lastFloorIndex = (lastFloorIndex + 1) % floors.Length;

			lastFloorX += floorWidth;
			floor.transform.position = new Vector2(lastFloorX + floorWidth, 0);
			floor.Reset();
		}
	}
}
