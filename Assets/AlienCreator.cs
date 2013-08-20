using UnityEngine;
using System.Collections;

public class AlienCreator : MonoBehaviour {
	
	public GameObject alienPrefab;
	
	public Texture alien1;
	
	public Texture alien2;
	
	public Texture alien3;
	
	public Texture mothership;
	
	public float distanceBetweenAliens = 15;
	
	void Start () {
		createAlienLine(alien1, 60);
		createAlienLine(alien2, 45);
		createAlienLine(alien2, 30);
		createAlienLine(alien3, 15);
		createAlienLine(alien3, 0);
	}
	
	private void createAlienLine(Texture texture, float y) {
		
		for (int i = 0; i < 10; i++) {	
			GameObject alien = (GameObject) GameObject.Instantiate(alienPrefab, new Vector3(distanceBetweenAliens * (i - 5), y, 300), Quaternion.identity);
			alien.renderer.material.mainTexture = texture;
		}
	}

}
