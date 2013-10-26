using UnityEngine;
using System.Collections;

public class GameMenuManager : MonoBehaviour
{
	public GUISkin skin;
	public Texture2D background;
	public Texture2D overlay;
	public int popupWidth = 450;
	public int popupHeight = 150;
	public World world;
	private WorldState state = WorldState.Loading;
	
	void Start() {
		world = World.Instance;
	}
	
	void OnGUI ()
	{
		GUI.skin = skin;
		
		GUI.DrawTexture(new Rect (0, 0, Screen.width, 40), overlay);
	
		switch (state) {
		case WorldState.Loading:
			//GUI.Label(new Rect((Screen.width - 80) / 2, (Screen.height - 40) / 2, 80, 40), "Level " + world.GetCurrentLevel(), "gameMessage");
			break;
		case WorldState.Won:
			GUI.Label(new Rect((Screen.width - 80) / 2, (Screen.height - 40) / 2, 80, 40), "You Won!", "gameMessage");
			showGameOverOptions();
			break;
		case WorldState.Lost:
			GUI.Label(new Rect((Screen.width - 80) / 2, (Screen.height - 40) / 2, 80, 40), "Mess with the best, die like the rest!\n Game Over!", "gameMessage");
			showGameOverOptions();
			break;
		case WorldState.Pause:
			ShowPause();
			break;
		}
		
		GUI.Label(new Rect(5, 0, 80, 40), "HP: " + world.PlayerHP(), "gameHUD");
		GUI.Label(new Rect(Screen.width/2 - 40, 0, 80, 40), "LEVEL " + world.GetCurrentLevel(), "gameHUD");
	}
	
	public void Pause ()
	{
		state = WorldState.Pause;
	}
	
	public void Unpause ()
	{
		state = WorldState.Playing;
	}
	
	public void LoadingLevel()
	{
		state = WorldState.Loading;
	}
	
	public void LevelLoaded()
	{
		state = WorldState.Playing;
	}
	
	public void GameOver(bool won)
	{
		if (won) {
			state = WorldState.Won;
		} else {
			state = WorldState.Lost;
		}
	}
		
	private void showGameOverOptions() {
		Rect backRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 60);
		Rect restartRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 200, 60);
		if (GUI.Button(backRect, "Back to menu", "gameMessage")) {
			Application.LoadLevel(0);
		}

		if (GUI.Button(restartRect, "Restart", "gameMessage")) {
			world.RestartLevel();
		}
	}

	private void ShowPause ()
	{
		Rect popupRect = new Rect ((Screen.width - popupWidth) / 2, (Screen.height - popupHeight) / 2, popupWidth, popupHeight);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), overlay);
		GUI.DrawTexture (popupRect, background);
		
		GUILayout.BeginArea (popupRect);
		
		GUILayout.Label ("Game paused", "pauseTitle");
		
		if (GUILayout.Button ("Continue", "pauseButton")) {
			world.Unpause ();
		}
		
		if (GUILayout.Button ("Exit", "pauseButton")) {
			Application.LoadLevel(0);
		}
		
		GUILayout.EndArea ();
	}
}
