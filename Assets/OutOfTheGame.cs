using UnityEngine;
using System.Collections;

public class OutOfTheGame : MonoBehaviour {
	void OnCollitionEnter2D(Collision2D col) {
		if (col.collider.tag == "Play") {
			WorldController.instance.Reset();
			Debug.Log("Out of the game");
		}
	}
}
