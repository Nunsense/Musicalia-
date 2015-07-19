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

public enum Notes { 
	C = 0,
	Cs = 1,
	D = 2, 
	Ds = 3, 
	E = 4,
	F = 5,
	Fs = 6,
	G = 7,
	Gs = 8,
	A = 9,
	As = 10,
	B = 11,
	C2 = 12,
	C2s = 13,
	D2 = 14, 
	D2s = 15, 
	E2 = 16,
	F2 = 17,
	F2s = 18,
	G2 = 19,
	G2s = 20,
	A2 = 21,
	A2s = 22,
	B2 = 23
}

public class PlatformController : MonoBehaviour {
	public int type;
	public MovementModifiers movementModifier;
	public PlayerModifiers playerModifier;
	public Notes note;
	public float delayTime = 1;
	public float maxPos;
	public float minPos;
	
	SpriteRenderer renderer;
	Animator anim;
	bool modifierActivated = false;
	float waitingTime = 0;
	Rigidbody2D rb2d;
	Transform trans;
	AudioSource audio;
	
	void Awake() {
		renderer = GetComponentInChildren<SpriteRenderer>();
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		trans = transform;
	}
	
	public void Initialize(Notes _note, int _type, PlayerModifiers _playerModifier, MovementModifiers _movementModifier) {
		note = _note;
		type = _type;
		playerModifier = _playerModifier;
		movementModifier = _movementModifier;
	}
	
	public void Touch() {
		if (type != 666) {
			audio.Play();
			PianoController.Instance.Notes[(int)note].Play ();
			if (anim) 
				anim.SetTrigger("Touch");
		
			modifierActivated = true;
		}	
	}
	
	void Start () {
		audio = Instantiate(PianoController.Instance.Notes[(int)note]);
		Run();
	}
	
	void Update() {
	 	switch (movementModifier) {
		case MovementModifiers.patrolHorizontal : {
			if (trans.position.x >= maxPos) {
				rb2d.velocity = new Vector2(-1, 0);
			} else if (trans.position.x <= minPos) {
				rb2d.velocity = new Vector2(1, 0);
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
		Run();
	}
	
	public void SetColor(Color color) {
		color.a = 1f;
		renderer.color = color;
	}
	
	void Run() {
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
}