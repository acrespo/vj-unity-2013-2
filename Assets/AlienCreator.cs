using UnityEngine;
using System.Collections;

public class AlienCreator : MonoBehaviour {
	
	public GameObject alienPrefab;
	
	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < 10; i++) {	
			GameObject.Instantiate(alienPrefab, new Vector3(30 * (i - 5), 0, 300), Quaternion.identity);
		}
		// Z = 300, Y = 0, X = k * 15
	}
}
