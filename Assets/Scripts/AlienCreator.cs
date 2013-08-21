using UnityEngine;
using System;
using System.Collections;

public class AlienCreator : MonoBehaviour {
	
	public GameObject alienPrefab;
	
	public AlienData alien1;
	
	public AlienData alien2;
	
	public AlienData alien3;
	
	public AlienData mothership;
	
	public float distanceBetweenAliens = 15;
	
	void Start () {
		createAlienLine(alien1, 60);
		createAlienLine(alien2, 45);
		createAlienLine(alien2, 30);
		createAlienLine(alien3, 15);
		createAlienLine(alien3, 0);
	}
	
	private void createAlienLine(AlienData data, float y) {
		
		for (int i = 0; i < 10; i++) {	
			GameObject alien = (GameObject) GameObject.Instantiate(alienPrefab, new Vector3(distanceBetweenAliens * (i - 5), y, 300), Quaternion.identity);
			alien.renderer.material.mainTexture = data.texture;
			alien.GetComponent<KillMe>().score = data.score;
		}
	}
	
	[Serializable]
	public class AlienData {
		
		public Texture texture;
		
		public int score;
		
	}

}
