﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GuiManager gui;
	public CoffeLevelImage coffeLevelImage;
	public CameraController cameraController;
	public WorldController world;
	int coffeeLevel;
	int prevCoffeeLevel;
	PlayerMovement movement;
	float coffeeTimeElapsed;
	float coffeeTime = 5; //5 seconds
	Animator anim;

	void Awake() {
		coffeeTimeElapsed = coffeeTime;
		anim = GetComponent<Animator>();
		prevCoffeeLevel = 1;
		coffeeLevel = 1;
		movement = GetComponent<PlayerMovement>();
	}

	void Start() {
	
	}

	void Update() {
		if(transform.position.y < -0.5f) {
			Die();
		}
		
		coffeeTimeElapsed -= Time.deltaTime;
		if(coffeeTimeElapsed <= 0) {
			ConsumeCoffee();
			coffeeTimeElapsed = coffeeTime;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "coffeeBean") {
			SoundManager.instance.playCoffee();
			coffeeLevel += 1;
			col.gameObject.SetActive(false);

			if(coffeeLevel == 4)
				Die();
			else
				UpdateSpeed();
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if(col.collider.tag == "goblin") {
			col.collider.isTrigger = true;
			if(coffeeLevel == 3) {
				SoundManager.instance.playPunch();
				col.rigidbody.AddForce((col.transform.position - transform.position) * Random.Range(400, 600) + col.transform.up * Random.Range(400, 600));
			} else {
				SoundManager.instance.playGoblin();
				Die();
			}
		} else if(col.collider.tag == "oldLady") {
			col.collider.isTrigger = true;
			if(coffeeLevel == 0) {
				SoundManager.instance.playOldLady();
				Die();
			} else if(coffeeLevel == 3) {
				SoundManager.instance.playPunch();
				col.rigidbody.AddForce((col.transform.position - transform.position) * Random.Range(400, 600) + col.transform.up * Random.Range(400, 600));
			} else {
				SoundManager.instance.playOldLady();
				col.rigidbody.isKinematic = true;
				coffeeLevel--;
				UpdateSpeed();
				UpdateUI();
			}
		} else if(col.collider.tag == "elf") {
			col.collider.isTrigger = true;
			if(coffeeLevel == 0) {
				SoundManager.instance.playOldLady();
				Die();
			} else if(coffeeLevel == 3) {
				SoundManager.instance.playPunch();
				col.rigidbody.AddForce((col.transform.position - transform.position) * Random.Range(400, 600) + col.transform.up * Random.Range(400, 600));
			} else {
				SoundManager.instance.playPigeon();
				col.rigidbody.isKinematic = true;
				coffeeLevel--;
				UpdateSpeed();
				UpdateUI();
			}
		} else if(col.collider.tag == "ground") {
			if(movement.VerticalSpeed() > 0) {
				col.collider.isTrigger = true;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if(col.tag == "ground") {
			col.isTrigger = false;
		}
	}
	
	void UpdateSpeed() {
		coffeeTimeElapsed = coffeeTime;
		anim.SetInteger("speed", coffeeLevel);
		switch (coffeeLevel) {
		case 0:
			world.TransformUp();
			movement.SetSlowSpeed();
			cameraController.SetNoneBlur();
			break;
		case 1:
			if (prevCoffeeLevel == 0)
				world.TransformDown();
			movement.SetNormalSpeed();
			cameraController.SetNoneBlur();
			break;
		case 2:
			movement.SetFastSpeed();
			cameraController.SetMinBlur();
			break;
		case 3:
			movement.SetSuperFastSpeed();
			cameraController.SetMaxBlur();
			break;
		}
		UpdateUI();
		prevCoffeeLevel = coffeeLevel;
	}
	
	void Die() {
		anim.SetTrigger("dead");
	}
	
	void Dead() {
		gui.EndGame(movement.distance);
	}

	void UpdateUI() {
		coffeLevelImage.UpdateLevels(coffeeLevel);
	}

	public void Reset() {
		movement.Reset();
		coffeeLevel = 1;
		movement.SetNoSpeed();
		UpdateUI();
		anim.SetBool("grounded", false);
		anim.SetTrigger("wake_up");
	}
	
	public void TriggerJump() {
		anim.SetTrigger("jump");
		anim.SetBool("grounded", false);
	}
	
	public void TriggerGrounded() {
		anim.SetBool("grounded", true);
	}

	public int GetCoffee() {
		return coffeeLevel;
	}

	public void ConsumeCoffee() {
		if (coffeeLevel > 0) {
			coffeeLevel--;
			UpdateSpeed();
			UpdateUI();
		}
	}
}
