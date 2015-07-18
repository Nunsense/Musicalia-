using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformController : MonoBehaviour {
	
	int num;
	int type;
	int modifier;
	
	public void Initialize(int _num, int _type, int _modifier) {
		num = _num;
		type = _type;
		modifier = _modifier;
	}
	
	void Start () {
	
	}
}