using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public GUISkin skin;
	public Texture2D background;
	private string state = "main";
	private float baseX = 50;
	private float outsideLeftX = -200;
	private float outsideRightX = 640;
	private float mainX;
	private float otherX;
	
	void Start ()
	{
		mainX = baseX;
		otherX = baseX;
	}
	
	void OnGUI ()
	{
		GUI.skin = skin;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), background);
		GUI.Label (new Rect (0, 0, 100, 100), "Tank City 3D", "title");
		
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
		case "transitionOptions":
			ShowMain ();
			ShowOptions ();
			mainX = Mathf.SmoothStep (mainX, outsideLeftX, Time.deltaTime * 10);
			otherX = Mathf.SmoothStep (otherX, baseX, Time.deltaTime * 10);
			if (mainX - outsideLeftX < 0.1f && otherX - baseX < 0.1f) {
				state = "options";
			}
			break;
		case "transitionOptionsBack":
			ShowMain ();
			ShowOptions ();
			mainX = Mathf.SmoothStep (mainX, baseX, Time.deltaTime * 10);
			otherX = Mathf.SmoothStep (otherX, outsideRightX, Time.deltaTime * 10);
			if (baseX - mainX < 0.1f && outsideRightX - otherX < 0.1f) {
				state = "main";
			}
			break;
		case "transitionCredits":
			ShowMain ();
			ShowCredits ();
			mainX = Mathf.SmoothStep (mainX, outsideLeftX, Time.deltaTime * 10);
			otherX = Mathf.SmoothStep (otherX, baseX, Time.deltaTime * 10);
			if (mainX - outsideLeftX < 0.1f && otherX - baseX < 0.1f) {
				state = "credits";
			}
			break;
		case "transitionCreditsBack":
			ShowMain ();
			ShowCredits ();
			mainX = Mathf.SmoothStep (mainX, baseX, Time.deltaTime * 10);
			otherX = Mathf.SmoothStep (otherX, outsideRightX, Time.deltaTime * 10);
			if (baseX - mainX < 0.1f && outsideRightX - otherX < 0.1f) {
				state = "main";
			}
			break;
		}
	}
	
	private void ShowMain ()
	{
		GUILayout.BeginArea (new Rect (mainX, 160, 400, 400));
		
		if (GUILayout.Button ("Start game", "button")) {
			Application.LoadLevel (1);
		}
		if (GUILayout.Button ("Options", "button")) {
			state = "transitionOptions";
			mainX = baseX;
			otherX = outsideRightX;
		}
		if (GUILayout.Button ("Credits", "button")) {
			state = "transitionCredits";
			mainX = baseX;
			otherX = outsideRightX;
		}
		
		GUILayout.EndArea ();
	}
	
	private void ShowOptions ()
	{
		GUILayout.BeginArea (new Rect (otherX, 160, 400, 400));
		
		GUILayout.Label ("Options");
		
		bool useSound = PlayerPrefs.GetInt ("sound", 1) == 1;
		useSound = GUILayout.Toggle (useSound, "  Sound effects");
		PlayerPrefs.SetInt ("sound", useSound ? 1 : 0);
		
		if (GUILayout.Button ("Back", "button")) {
			state = "transitionOptionsBack";
			mainX = outsideLeftX;
			otherX = baseX;
		}
		
		GUILayout.EndArea ();
	}
	
	private void ShowCredits ()
	{
		GUILayout.BeginArea (new Rect (otherX, 160, 400, 400));
		
		GUILayout.Label ("Credits");
		
		GUILayout.Label ("Juan");
		GUILayout.Label ("Alvaro");
		GUILayout.Label ("Frederic");
		
		if (GUILayout.Button ("Back", "button")) {
			state = "transitionCreditsBack";
			mainX = outsideLeftX;
			otherX = baseX;
		}
		
		GUILayout.EndArea ();
	}
}
