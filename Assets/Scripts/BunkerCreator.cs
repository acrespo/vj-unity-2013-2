using UnityEngine;
using System.Collections;

public class BunkerCreator : MonoBehaviour {

	public GameObject bunkerPrefab;
	
	public Texture bunkerTexture;
	
	public float distanceBetweenBunkers = 50;
	
	// Use this for initialization
	void Start () {
		createBunkerLine(bunkerTexture, -30);
	}
	
	void createBunkerLine(Texture bunkerTexture, float y) {
		
		for (int i = 0; i < 4; i++) {	
			GameObject bunker = (GameObject) GameObject.Instantiate(bunkerPrefab, new Vector3(distanceBetweenBunkers * (i - 2), y, 300), Quaternion.identity);
			bunker.renderer.material.mainTexture = bunkerTexture;			
		}
	}
}
