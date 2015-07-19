using UnityEngine;
using System.Collections;

public class Backgrounds : MonoBehaviour {

	public Sprite sprites;
	public float multiplier;
	public float distance;
	
	SpriteRenderer renderer;
	Transform player;
	Transform[] childs;
	float[] positions;
	float prevPlayerPos = 0;

	void Awake () {
		renderer = GetComponent<SpriteRenderer>();
		childs = GetComponentsInChildren<Transform>();
		player = FindObjectOfType<PlayerController>().transform;
		positions = new float[childs.Length];
		Vector3 pos;
		for (int i = 0; i < childs.Length; i++) {
			pos = childs[i].position;
			pos.x = (i - 1) * distance;
			childs[i].position = pos;
			positions[i] = pos.x;
		}
	}

	void FixedUpdate () {
		Vector3 pos;
		float diff = prevPlayerPos - player.position.x;
		prevPlayerPos = player.position.x;
		for (int i = 0; i < childs.Length; i++) {
			pos = childs[i].position;
			pos.x += diff * multiplier;
			Debug.Log(diff);
			
			if (pos.x <= -distance) {
				pos.x = positions[i];
			}
			childs[i].position = pos;
		}
	}
}
