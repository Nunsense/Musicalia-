using UnityEngine;
using System.Collections;

public class EndGameMenuController : MonoBehaviour {
	void Start() {
	
	}

	public void OpenGame() {
		Application.LoadLevel(0);
	}
}
