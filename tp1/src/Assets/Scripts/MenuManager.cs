using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public GUISkin skin;
	public Texture2D background;
	private string state = "main";

	void OnGUI ()
	{
		GUI.skin = skin;
		GUI.DrawTexture (new Rect (Screen.width / 2 - 80, 50, 160, 160), background);
		
		switch (state) {
		case "main":
			ShowMain ();
			break;
		case "options":
			ShowOptions ();
			break;
		case "credits":
			ShowCredits ();
			break;
		}
	}
	
	private void ShowMain ()
	{
		GUILayout.BeginArea (new Rect (Screen.width / 2 - 50, Screen.height / 2, 100, 100));
		if (GUILayout.Button ("Start game")) {
			Application.LoadLevel (1);
		}
		if (GUILayout.Button ("Options")) {
			state = "options";
		}
		if (GUILayout.Button ("Credits")) {
			state = "credits";	
		}
		GUILayout.EndArea ();
	}
	
	private void ShowOptions ()
	{
		GUI.Label (new Rect (Screen.width / 2 - 52, Screen.height / 2, 104, 20), "== Options ==");
		
		bool useSound = PlayerPrefs.GetInt ("sound", 1) == 1;
		useSound = GUI.Toggle (new Rect (Screen.width / 2 - 70, Screen.height / 2 + 40, 140, 20), useSound, "  Sound effects");
		PlayerPrefs.SetInt ("sound", useSound ? 1 : 0);
		
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 80, 100, 20), "Back")) {
			state = "main";	
		}
	}
	
	private void ShowCredits ()
	{
		GUI.Label (new Rect (Screen.width / 2 - 52, Screen.height / 2, 104, 20), "== Credits ==");
		
		GUI.Label (new Rect (Screen.width / 2 - 48, Screen.height / 2 + 40, 96, 20), "Juan Civile");
		GUI.Label (new Rect (Screen.width / 2 - 58, Screen.height / 2 + 60, 116, 20), "Alvaro Crespo");
		GUI.Label (new Rect (Screen.width / 2 - 92, Screen.height / 2 + 80, 184, 20), "Frederic Schertenleib");
		
		if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 120, 100, 20), "Back")) {
			state = "main";	
		}
	}
}
