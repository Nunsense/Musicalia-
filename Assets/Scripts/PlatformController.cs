using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerModifiers { 
	none = 0,
	longJump = 1
}

public enum MovementModifiers { 
	none = 0,
	upOnContact = 1,
	downOnContact = 2, 
	downOnContactWithDelay = 3, 
	patrolVertical = 4,
	patrolHorizontal = 5
}

public class PlatformController : MonoBehaviour {
	
	
	public int num;
	public int type;
	public MovementModifiers movementModifier;
	public PlayerModifiers playerModifier;
	public int note;
	public float delayTime = 1;
	public float maxPos;
	public float minPos;
	
	SpriteRenderer renderer;
	Animator anim;
	bool modifierActivated = false;
	float waitingTime = 0;
	Rigidbody2D rb2d;
	Transform trans;
	
	void Awake() {
		renderer = GetComponentInChildren<SpriteRenderer>();
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		trans = transform;
	}
	
	public void Initialize(int _num, int _type, PlayerModifiers _playerModifier, MovementModifiers _movementModifier) {
		num = _num;
		type = _type;
		playerModifier = _playerModifier;
		movementModifier = _movementModifier;
	}
	
	public void Touch() {
		PianoController.Instance.Notes [num].Play ();
		anim.SetTrigger("Touch");
		modifierActivated = true;
	}
	
	void Start () {
		switch (movementModifier) {
		case MovementModifiers.patrolHorizontal : {
			rb2d.velocity = new Vector2(1, 0);
			break;
		}
		case MovementModifiers.patrolVertical : {
			rb2d.velocity = new Vector2(0, -1);
			break;
		}
		}
	}
	
	void Update() {
	 	switch (movementModifier) {
		case MovementModifiers.patrolHorizontal : {
			if (trans.position.x >= maxPos) {
				rb2d.velocity = new Vector2(1, 0);
			} else if (trans.position.x <= minPos) {
				rb2d.velocity = new Vector2(-1, 0);
			}
			break;
		}
		case MovementModifiers.patrolVertical : {
			if (trans.position.y >= maxPos) {
				rb2d.velocity = new Vector2(0, -1);
			} else if (trans.position.y <= minPos) {
				rb2d.velocity = new Vector2(0, 1);
			}
			break;
		}
	 	}
	 	
	 	if (modifierActivated) {
			switch (movementModifier) {
			case MovementModifiers.upOnContact : {
				rb2d.velocity = new Vector2(0, 1);
				break;
			}
			case MovementModifiers.downOnContact : {
				rb2d.velocity = new Vector2(0, -1);
				break;
			}
			case MovementModifiers.downOnContactWithDelay : {
				waitingTime += Time.deltaTime;
				if (waitingTime >= delayTime) {
					rb2d.velocity = new Vector2(0, -1);
				}
				break;
			}
			}
	 	}
	}
	
	public void Reset() {
		gameObject.SetActive(true);
		modifierActivated = false;
		waitingTime = 0;
		rb2d.velocity = Vector2.zero;
		Transform coin = transform.FindChild("Coin");
		if (coin) {
			coin.gameObject.SetActive(true);
		}
	}
}