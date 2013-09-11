using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	
	public GameObject mapBlock;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			addBlock(10, i);
		}
	}
	
	
	private void addBlock(int x, int z) {
		GameObject block = (GameObject) GameObject.Instantiate(mapBlock);
		block.transform.position = new Vector3(x, 0.51f, z);
	}
}
