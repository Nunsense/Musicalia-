using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformController : MonoBehaviour {
	
	public int num;
	public int type;
	public int modifier;
	public int note;
	
	SpriteRenderer renderer;
	Animator anim;
	
	void Awake() {
		renderer = GetComponentInChildren<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}
	
	public void Initialize(int _num, int _type, int _modifier) {
		num = _num;
		type = _type;
		modifier = _modifier;
	}
	
	public void Touch() {
		PianoController.Instance.Notes [num].Play ();
		anim.SetTrigger("Touch");
	}
	
	void Start () {

	}
	
	public void Reset() {
		gameObject.SetActive(true);
		Transform coin = transform.FindChild("Coin");
		if (coin) {
			coin.gameObject.SetActive(true);
		}
	}
}