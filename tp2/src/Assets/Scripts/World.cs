using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	
	public GameObject mapBlock;
	
	public GameObject tank;
	
	private bool paused = false;
	
	public TextAsset firstLevel;

	// Use this for initialization
	void Start () {
		
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
		
		string text = firstLevel.text;
		
		int i = 0, j = 0;
		foreach (char c in text) {
			if (c == '\n') {
				j++;
				i = -1;
			} else if (c == '#') {
				addBlock(i, j);
			} else if (c == 'P') {
				GameObject tankObj = Instantiate(tank) as GameObject;
				tankObj.transform.position = new Vector3(i, 0.51f, j);
			}
			
			i++;
		}
		
	}
	
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) {

			if (paused) {
				Time.timeScale = 1;
			} else {
				Time.timeScale = 0;
			}

			paused = !paused;
		}
	}
	
	private void addBlock(int x, int z) {
		GameObject block = Instantiate(mapBlock) as GameObject;
		block.transform.position = new Vector3(x, 0.51f, z);
	}
}
