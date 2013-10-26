using System.IO;
using System.Text;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	
	public GameMenuManager gameMenuManager;
//	private EnemyManager enemyManager;
	private bool paused = false;
	private int currentLevel;
	public Player player;
	private float previousTimeScale = 1;
	
	
		void Start () {
//		enemyManager = new EnemyManager(transform, () => LoadNextLevel());
		
		currentLevel = 0;
		LoadNextLevel();
	}
	
	public float PlayerHP() {
		return player.GetLife();
	}
	
	void LoadNextLevel() {
		//TODO: Actually load next level
	}
	
	
	public void RestartLevel() {
		//TODO: Restart
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
	
	void OnGUI() {
		
		if (GUI.Button(new Rect(0, 0, 20, 20), "Next")) {
			LoadNextLevel();
		}
	}
	
	
	public void Pause() {
		paused = true;
		previousTimeScale = Time.timeScale;
		Time.timeScale = 0;
		gameMenuManager.Pause();
	}
	
	public void Unpause() {
		paused = false;
		Time.timeScale = previousTimeScale;
		gameMenuManager.Unpause();
	}
	
//	public EnemyManager GetEnemyManager() {
//		return enemyManager;
//	}
	
	public int GetCurrentLevel() {
		return currentLevel;
	}
	
}

