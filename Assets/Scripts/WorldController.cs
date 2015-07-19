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
	
//	class Plato {
//		public int Num { get; set; } 
//		public int Type { get; set; }
//		public int Modifier { get; set; }
//		
//		public Plato(int _num, int _type, int _modifier) {
//			Num = _num;
//			Type = _type;
//			Modifier = _modifier;
//		}
//	}
	
//	List<PlatformController> LevelGenerator(string json) {
//		plataforms = new List<PlatformController>();
//		
//		List<Plato> platos = new List<Plato>();
//		for (int i = 0; i < 20; i ++) {
//			int type = Random.Range(1, 4);
//			int num = Random.Range(0, 2);
//			int modifier = 0;
//			if (type == 3) {
//				modifier = 1;
//			}
//			platos.Add(new Plato(num, type, modifier));
//			if (type == 3) {
//				platos.Add(new Plato(0, 0, 0));
//			}
//		}
//
//		for (int i = 0; i < platos.Count; i ++) {
//			Plato plato = platos[i];
//			
//			GameObject plataform = null;
//			
//			switch (plato.Type) {
//			case 1 : {
//				plataform = GameObject.Instantiate(groundNormal);
//				break;	
//			}
//			case 2 : {
//				plataform = GameObject.Instantiate(groundFall);
//				break;	
//			}
//			case 3 : {
//				plataform = GameObject.Instantiate(groundJump);
//				break;	
//			}
//			}
//			if (plataform) {
//				PlatformController controller = plataform.GetComponent<PlatformController>();
//				controller.Initialize(plato.Num, plato.Type, plato.Modifier, 0);
//				plataform.transform.position = new Vector3(i * groundWidth, plato.Num * heightMultiplier, 0);
//				plataforms.Add(controller);
//			}
//		}
//		
//		return plataforms;
//	}
//	
//	public void StartLevel(string json) {
//		LevelGenerator(json);
//	}
	
	public void Reset() {
		for (int i = 0; i < plataforms.Length; i ++) {
			plataforms[i].Reset();
		}
		player.Reset();
		coinsTaken = 0;
		tintAnim.SetInteger("step", 0);
	}
	
	public void AddCoin() {
		coinsTaken ++;
		Debug.Log("Coins: " + coinsTaken);
		
		float advance = totalCoins / coinsTaken;
		if (advance > 0.25f) {
			tintAnim.SetInteger("step", 1);
		} else if (advance > 0.55f) {
			tintAnim.SetInteger("step", 2);
		} else if (advance > 0.9f) {
			tintAnim.SetInteger("step", 3);
		}
	}
}
