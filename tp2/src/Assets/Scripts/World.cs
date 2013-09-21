﻿using System.IO;
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
	
	private LevelLoadAnimation loadLevelAnimation;
	
	private float previousScale = 1;
	
	// Use this for initialization
	void Start () {
		enemyManager = new EnemyManager(transform, () => LoadNextLevel());
		
		lives = 3;
		currentLevel = 0;
		LoadNextLevel();
	}
	
	public int PlayerLives {
		get {
			return lives;
		}
		set {
			lives = value;
		}
	}
	
	public bool LevelLoading {
		get {
			return loadLevelAnimation != null;
		}
	}
	
	void LoadNextLevel() {
		currentLevel++;
		
		string fileName = "Assets/Levels/" + currentLevel + ".txt";
		if (File.Exists(fileName)) {
			LoadLevel(fileName);
		} else {
			Time.timeScale = 0;
			currentLevel = -1;
		}
	}

	void LoadLevel (string fileName)
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
	
		enemyManager.Spawn();
		
		Time.timeScale = 0;
		loadLevelAnimation = new LevelLoadAnimation(CenterCamera(width, j));
		
	}
	
	private Vector3 CenterCamera(int width, int height) {
		return new Vector3(width / 2.0f, (height + 5) * 0.8f, (height - 3) * 0.2f);
	}
	
	void TowerDestroyed(GameObject tower) {
		Time.timeScale = 0;
	}

	void clearLevel() {
		
		enemyManager.Reset();
		foreach (Transform child in transform) {
			ObjectPool.Instance.Return(child.gameObject);
		}
		
	}
	
	void Update () {
		
		if (loadLevelAnimation != null) {
			if (!loadLevelAnimation.Update()) {
				Time.timeScale = 1;
				loadLevelAnimation = null;
			}
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			if (paused) {
				Unpause();
			} else {
				Pause();
			}
		}
	}
	
	void OnGUI() {
		
		if (GUI.Button(new Rect(0, 0, 20, 20), "Next")) {
			LoadNextLevel();
		}
	}
	
	public void Pause() {
		paused = true;
		previousScale = Time.timeScale;
		Time.timeScale = 0;
		gameMenuManager.Pause();
	}
	
	public void Unpause() {
		paused = false;
		Time.timeScale = previousScale;
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
