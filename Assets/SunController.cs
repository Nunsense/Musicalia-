using UnityEngine;
using System.Collections;

public class SunController : MonoBehaviour {

	Transform sun;
	
	void Awake () {
		sun = GetComponentInChildren<Transform>();
//		sun.transform.position = 
	}

	void FixedUpdate () {
			
	}
}
