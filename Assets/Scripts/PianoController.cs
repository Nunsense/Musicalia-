using UnityEngine;
using System.Collections;

public class PianoController : MonoBehaviour {

	public static PianoController Instance;
	public AudioSource[] Notes;

	void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
