using UnityEngine;
using System.Collections;

public class GameMenuManager : MonoBehaviour
{
	public GUISkin skin;
	public Texture2D background;
	public Texture2D overlay;
	public int popupWidth = 450;
	public int popupHeight = 150;
	public GameObject map;
	private World world;
	private string state = "game";
	
	void Awake ()
	{
		world = map.GetComponent<World> ();
	}
	
	void OnGUI ()
	{
		GUI.skin = skin;
		
		switch (state) {
		case "game":
			ShowGame ();
			break;
		case "pause":
			ShowPause ();
			break;
		}
	}
	
	public void Pause ()
	{
		state = "pause";
	}
	
	public void Unpause ()
	{
		state = "game";
	}
	
	private void ShowGame ()
	{
		GUI.DrawTexture (new Rect (0, 0, Screen.width, 40), overlay);
		GUI.Label(new Rect(12, 12, 80, 40), "Enemies left: " + world.GetEnemyManager().GetTanksLeft(), "gameHUD");
		GUI.Label(new Rect(Screen.width/2 - 40, 12, 80, 40), "Level " + world.GetCurrentLevel(), "gameHUD");
		GUI.Label(new Rect(Screen.width - 80, 12, 80, 40), "Lifes: " + world.PlayerLives, "gameHUD");
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
			Application.LoadLevel (0);
		}
		
		GUILayout.EndArea ();
	}
}
