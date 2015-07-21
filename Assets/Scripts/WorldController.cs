using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldController : MonoBehaviour {
	public static WorldController instance;

	public GameObject groundNormal;
	public GameObject groundFall;
	public GameObject groundJump;
	
	public float heightMultiplier;
	public float groundWidth;
	
	public Animator tintAnim;
	
//	private List<PlatformController> plataforms;
	PlatformController[] plataforms;
	private PlayerController player;
	
	[SerializeField] int totalCoins = 0;
	[SerializeField] int coinsTaken = 0;
	
	public Color[] colors;
	
	void Awake() {
		instance = this;
		player = FindObjectOfType<PlayerController>();
	}
	
	void Start () {
		plataforms = FindObjectsOfType<PlatformController>();
		for (int i = 0; i < plataforms.Length; i ++) {
			if (plataforms[i].type == 5) {
				totalCoins ++;
				plataforms[i].SetColor(colors[Random.Range(0, colors.Length)]);
			}
		}
//		StartLevel("");
	}
	
	void Update () {
	
	}
	
	public Color RandomColor() {
		return colors[Random.Range(0, colors.Length)];
	}
	
	public void Reset() {
		for (int i = 0; i < plataforms.Length; i ++) {
			plataforms[i].Reset();
		}
		player.Reset();
		coinsTaken = 0;
	}
	
	public void AddCoin() {
		coinsTaken ++;
		Debug.Log("Coins: " + coinsTaken);
		
		float advance = totalCoins / coinsTaken;
	}
}
