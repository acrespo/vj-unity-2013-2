using System.IO;
using System.Text;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	
	public GameObject tank;
	
	private bool paused = false;
	
	private EnemyManager enemyManager;

	// Use this for initialization
	void Start () {
		enemyManager = new EnemyManager(transform, () => loadNextLevel());
		loadLevel ("Assets/Levels/first.txt");
	}
	
	void loadNextLevel() {
		loadLevel("Assets/Levels/first.txt");
	}

	void loadLevel (string fileName)
	{
		clearLevel ();
		
		StreamReader reader = new StreamReader(fileName, Encoding.ASCII);
		
		using (reader) {
			int i = 0, j = 0, width = 0;
			int c = -1;
			
			while ((c = reader.Read()) != -1) {
				if (c == '\n') {
					if (width == 0) {
						width = i;
					}
					addBlock("Unkillable", -1, j);
					addBlock("Unkillable", width, j);
					
					j++;
					i = -1;
					
				} else if (c == '#') {
					addBlock("Block", i, j);
				} else if (c == 'P') {
					GameObject tankObj = Instantiate(tank) as GameObject;
					tankObj.transform.position = new Vector3(i, 0.51f, j);
					tankObj.transform.parent = transform;
				} else if (c == 'M') {
					addBlock("Unkillable", i, j);
				} else if (c == 'E') {
					enemyManager.AddPoint(new Vector2(i, j));
				}
				
				i++;
			}
			
			addBlock("Unkillable", -1, j);
			addBlock("Unkillable", width, j);
			
			for (i = -1; i < width + 1; i++) {
				addBlock("Unkillable", i, -1);
				addBlock("Unkillable", i, j);
			}
		}
		
		enemyManager.Spawn();
	}

	void clearLevel() {
		
		enemyManager.Reset();
		foreach (Transform child in transform) {
			ObjectPool.Instance.Return(child.gameObject);
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
	
	private void addBlock(string type, int x, int z) {
		GameObject block = ObjectPool.Instance.GetObject(type);
		block.transform.position = new Vector3(x, 0.51f, z);
		block.transform.parent = transform;
	}
}
