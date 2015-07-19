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
	
//	private List<PlatformController> plataforms;
	private PlayerController player;
	
	void Awake() {
		instance = this;
		player = FindObjectOfType<PlayerController>();
	}
	
	void Start () {
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
		Object[] plataforms = FindObjectsOfType<PlatformController>();
		for (int i = 0; i < plataforms.Length; i ++) {
			(plataforms[i] as PlatformController).Reset();
		}
		Object[] coins = FindObjectsOfType<PlatformController>();
		for (int i = 0; i < coins.Length; i ++) {
			(coins[i] as PlatformController).Reset();
		}
		player.Reset();
	}
}
