using UnityEngine;
using System.Collections;

public class AlienCreator : MonoBehaviour {
	
	public GameObject alienPrefab;
	
	public Texture alien1;
	
	public Texture alien2;
	
	public Texture alien3;
	
	public Texture mothership;
	
	public float distanceBetweenAliens = 15;
	
	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < 10; i++) {	
			GameObject alien = (GameObject) GameObject.Instantiate(alienPrefab, new Vector3(distanceBetweenAliens * (i - 5), 60, 300), Quaternion.identity);
			alien.renderer.material.mainTexture = alien1;
		}
		
		for (int i = 0; i < 10; i++) {	
			GameObject alien = (GameObject) GameObject.Instantiate(alienPrefab, new Vector3(distanceBetweenAliens * (i - 5), 45, 300), Quaternion.identity);
			alien.renderer.material.mainTexture = alien2;
		}
		
		for (int i = 0; i < 10; i++) {	
			GameObject alien = (GameObject) GameObject.Instantiate(alienPrefab, new Vector3(distanceBetweenAliens * (i - 5), 30, 300), Quaternion.identity);
			alien.renderer.material.mainTexture = alien2;
		}
		
		for (int i = 0; i < 10; i++) {	
			GameObject alien = (GameObject) GameObject.Instantiate(alienPrefab, new Vector3(distanceBetweenAliens * (i - 5), 15, 300), Quaternion.identity);
			alien.renderer.material.mainTexture = alien3;
		}
		
		for (int i = 0; i < 10; i++) {	
			GameObject alien = (GameObject) GameObject.Instantiate(alienPrefab, new Vector3(distanceBetweenAliens * (i - 5), 0, 300), Quaternion.identity);
			alien.renderer.material.mainTexture = alien3;
		}
		// Z = 300, Y = 0, X = k * 15
	}

}
