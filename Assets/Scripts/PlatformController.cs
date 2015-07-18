using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformController : MonoBehaviour {
	
	public int Num { get; set; } 
	public int Type { get; set; }
	public int Modifier { get; set; }
	public int Note { get; set; }
	
	SpriteRenderer renderer;
	Animator anim;
	
	void Awake() {
		renderer = GetComponentInChildren<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}
	
	public void Initialize(int _num, int _type, int _modifier) {
		Num = _num;
		Type = _type;
		Modifier = _modifier;
	}
	
	public void Touch() {
		anim.SetTrigger("Touch");
		var transpose = -4;
		PianoController.Instance.Notes [Random.Range(0,12)].Play ();
	}
	
	void Start () {

	}
}