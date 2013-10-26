using System.IO;
using System.Text;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class World : Singleton<World> {
	
	public GameMenuManager gameMenuManager;
	private bool paused = false;
	private int currentLevel;
	public Player player;
	private float previousTimeScale = 1;
	
	
	void Start () {
		currentLevel = Application.loadedLevel;
	}
	
	public float PlayerHP() {
		return player.GetLife();
	}
	
	public void LoadNextLevel() {
		Application.LoadLevel(currentLevel+1);
	}
	
	public void RestartLevel() {
		Application.LoadLevel(currentLevel);
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
	
	public int GetCurrentLevel() {
		return currentLevel;
	}
	
}

