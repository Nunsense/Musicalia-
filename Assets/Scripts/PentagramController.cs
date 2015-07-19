using UnityEngine;
using System.Collections;

public class PentagramController : MonoBehaviour {


	void Awake() {
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 PlayerPOS = GameObject.Find("Player").transform.transform.position;
		Vector3 PentagramPOS = new Vector3(PlayerPOS.x, transform.position.y, transform.position.z);
		transform.position = PentagramPOS;
	}
}
