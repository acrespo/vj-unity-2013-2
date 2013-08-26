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
		createAlienLine(alien1, 70);
		createAlienLine(alien2, 55);
		createAlienLine(alien2, 40);
		createAlienLine(alien3, 25);
		createAlienLine(alien3, 10);
		
	}
	
	private void createAlienLine(AlienData data, float y) {
		
		for (int i = 0; i < 10; i++) {	
			GameObject alien = (GameObject) GameObject.Instantiate(alienPrefab, new Vector3(distanceBetweenAliens * (i - 5), y, 300), Quaternion.identity);
			alien.renderer.material.mainTexture = data.texture;
			alien.GetComponent<KillMe>().score = data.score;
			
			ScoreKeeper.Instance.AliensLeft += 1;
		}
	}
	
	[Serializable]
	public class AlienData {
		
		public Texture texture;
		
		public int score;
		
	}

}
