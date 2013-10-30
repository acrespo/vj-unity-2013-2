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
	private CharacterMotor mMotor;
	private FPSInputController mFPS;
	private MouseLook mMouseLook;
	private CharacterController mCharController;
	private MouseLook mCamMouseLook;
	
	
	void Start () {
		currentLevel = Application.loadedLevel;
		player = Player.Instance;
		gameMenuManager = GameMenuManager.Instance;
		mMotor = player.GetComponent<CharacterMotor>();
		mFPS = player.GetComponent<FPSInputController>();
		mMouseLook = player.GetComponent<MouseLook>();
		mCharController = player.GetComponent<CharacterController>();
		mCamMouseLook = Camera.main.GetComponent<MouseLook>();
		Unpause();
	}
	
	public float PlayerHP() {
		return player.GetLife();
	}
	
	public void LoadNextLevel() {
		if (currentLevel == 5) {
			paused = true;
			previousTimeScale = Time.timeScale;
			Time.timeScale = 0;
			setMouseLook(false);
			gameMenuManager.GameOver(true);	
		} else {
			Application.LoadLevel(currentLevel+1);
		}
	}
	
	public void RestartLevel() {
		Unpause();
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
		setMouseLook(!paused);
		gameMenuManager.Pause();
	}
	
	public void Unpause() {
		paused = false;
		Time.timeScale = previousTimeScale;
		setMouseLook(!paused);
		gameMenuManager.Unpause();
	}
	
	public void setMouseLook(bool b) {
		mFPS.enabled = b;
		mMotor.enabled = b;
		mMouseLook.enabled = b;
		mFPS.enabled = b;
		mCharController.enabled = b;
		mCamMouseLook.enabled = b;
	}
	public int GetCurrentLevel() {
		return currentLevel;
	}
	
}

