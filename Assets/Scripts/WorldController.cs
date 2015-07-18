using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldController : MonoBehaviour {
	public GameObject platform;
	
	void Start () {
		StartLevel("");
	}
	
	void Update () {
	
	}
	
	List<PlatformController> LevelGenerator(string json) {
		List<PlatformController> plataforms = new List<PlatformController>();
		
		for (int i = 0; i < 20; i ++) {
			GameObject plataform = GameObject.Instantiate(platform);
			PlatformController platforms = platform.GetComponent<PlatformController>();
			platforms.Initialize(2, 2, 0);
			
			platform.transform.position = new Vector3(i * 1, Random.Range(-2, 2), 0);
		}
		
		return plataforms;
	}
	
	public void StartLevel(string json) {
		LevelGenerator(json);
	}
}
