using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	
	public GameObject mapBlock;
	
	public GameObject unkillable;
	
	public GameObject tank;
	
	private bool paused = false;
	
	public TextAsset firstLevel;

	// Use this for initialization
	void Start () {
		
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
		
		string text = firstLevel.text;
		
		int i = 0, j = 0, width = 0;
		foreach (char c in text) {
			if (c == '\n') {
				if (width == 0) {
					width = i;
				}
				addBlock(unkillable, -1, j);
				addBlock(unkillable, width, j);
				
				j++;
				i = -1;
				
			} else if (c == '#') {
				addBlock(mapBlock, i, j);
			} else if (c == 'P') {
				GameObject tankObj = Instantiate(tank) as GameObject;
				tankObj.transform.position = new Vector3(i, 0.51f, j);
				tankObj.transform.parent = transform;
			} else if (c == 'M') {
				addBlock(unkillable, i, j);
			}
			
			i++;
		}
		
		addBlock(unkillable, -1, j);
		addBlock(unkillable, width, j);
		
		for (i = -1; i < width + 1; i++) {
			addBlock(unkillable, i, -1);
			addBlock(unkillable, i, j);
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
	
	private void addBlock(GameObject type, int x, int z) {
		GameObject block = Instantiate(type) as GameObject;
		block.transform.position = new Vector3(x, 0.51f, z);
		block.transform.parent = transform;
	}
}
