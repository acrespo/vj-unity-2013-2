using System.IO;
using System.Text;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	
	public GameObject tank;
	
	public GameMenuManager gameMenuManager;
	
	private bool paused = false;
	
	private int currentLevel;
	
	private EnemyManager enemyManager;
	
	private int lives;

	// Use this for initialization
	void Start () {
		enemyManager = new EnemyManager(transform, () => loadNextLevel());
		
		lives = 3;
		currentLevel = 0;
		loadNextLevel();
	}
	
	public int PlayerLives {
		get {
			return lives;
		}
		set {
			lives = value;
		}
	}
	
	void loadNextLevel() {
		currentLevel++;
		
		string fileName = "Assets/Levels/" + currentLevel + ".txt";
		if (File.Exists(fileName)) {
			loadLevel(fileName);
		} else {
			Debug.Log ("YOU WIN");
		}
	}

	void loadLevel (string fileName)
	{
		clearLevel ();
		
		StreamReader reader = new StreamReader(fileName, Encoding.ASCII);
		
		int i = 0, j = 0, width = 0;
		using (reader) {
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
					tankObj.transform.position = new Vector3(i, 0f, j);
					tankObj.transform.parent = transform;
				} else if (c == 'M') {
					addBlock("Unkillable", i, j);
				} else if (c == 'E') {
					enemyManager.AddPoint(new Vector2(i, j));
				} else if (c == 'T') {
					GameObject tower = ObjectPool.Instance.GetObject("Tower");
					tower.transform.parent = transform;
					tower.transform.position = new Vector3(i, 0f, j);
					
					tower.GetComponent<Destroyable>().onDestroy = TowerDestroyed;
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
	
		CenterCamera(width, j);
		
		enemyManager.Spawn();
	}
	
	private void CenterCamera(int width, int height) {
		
		Transform transform = Camera.main.transform;
		Vector3 pos = transform.position;
		pos.x = width / 2.0f;
		pos.z = (height - 3) * 0.2f;
		pos.y = (height + 5) * 0.8f;
		
		transform.position = pos;
	}
	
	void TowerDestroyed(GameObject tower) {
		Debug.Log ("YOU LOSE");
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
				Unpause();
			} else {
				Pause();
			}
		}
	}
	
	public void Pause() {
		paused = true;
		Time.timeScale = 0;
		gameMenuManager.Pause();
	}
	
	public void Unpause() {
		paused = false;
		Time.timeScale = 1;
		gameMenuManager.Unpause();
	}
	
	public EnemyManager GetEnemyManager() {
		return enemyManager;
	}
	
	public int GetCurrentLevel() {
		return currentLevel;
	}
	
	private void addBlock(string type, int x, int z) {
		GameObject block = ObjectPool.Instance.GetObject(type);
		block.transform.position = new Vector3(x, 0.51f, z);
		block.transform.parent = transform;
	}
}
