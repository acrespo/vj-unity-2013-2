using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public GUISkin skin;
	public float baseX = 0;
	public int titleFontSize = 34;
	private string state = "main";
	private float outsideLeftX;
	private float outsideRightX;
	private float mainX;
	private float otherX;
	
	void Start ()
	{
		outsideLeftX = -Screen.width;
		outsideRightX = Screen.width * 2;
		mainX = baseX;
		otherX = baseX;
	}
	
	void OnGUI ()
	{
		GUI.skin = skin;
		
		ShowCommon ();
		
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
	
	private void ShowCommon ()
	{
		// background
		GUI.Label (new Rect (0, 20, Screen.width, Screen.height), "", "window");
		
		// title bar
		int prevFontSize = skin.label.fontSize;
		skin.label.fontSize = titleFontSize;
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, 50));
		GUILayout.Label ("They are taking the hobbits to Isengard!", GUILayout.Height (50));
		GUILayout.EndArea ();
		skin.label.fontSize = prevFontSize;
		
		// decorative items
		AddSpikes ();
		FancyTop ();
		DeathBadge ();
		WaxSeal ();
	}
	
	private void ShowMain ()
	{
		GUILayout.BeginArea (new Rect ((Screen.width - 340) / 2 + mainX, (Screen.height - 80) / 2 + 20, 340, 80));
		
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
		GUILayout.BeginArea (new Rect ((Screen.width - 340) / 2 + otherX, (Screen.height - 100) / 2 + 20, 340, 100));
		
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
		GUILayout.BeginArea (new Rect ((Screen.width - 340) / 2 + otherX, (Screen.height - 160) / 2 + 20, 340, 160));
		
		GUILayout.Label ("Credits");
		
		GUILayout.Button ("Juan", "buttonLabelStyle");
		GUILayout.Button ("Alvaro", "buttonLabelStyle");
		GUILayout.Button ("Frederic", "buttonLabelStyle");
		
		if (GUILayout.Button ("Back", "button")) {
			state = "transitionCreditsBack";
			mainX = outsideLeftX;
			otherX = baseX;
		}
		
		GUILayout.EndArea ();
	}
	
	private void AddSpikes ()
	{
		int spikeWidth = 22;
		int numSpikes = (Screen.width - 100) / spikeWidth;
		int spikeAreaWidth = numSpikes * spikeWidth + 10;
		GUILayout.BeginArea (new Rect ((Screen.width - spikeAreaWidth) / 2, 65, spikeAreaWidth, 50));
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("", "SpikeLeft");
		for (int i = 0; i < numSpikes; i++) {
			GUILayout.Label ("", "SpikeMid");
		}
		GUILayout.Label ("", "SpikeRight");
		GUILayout.EndHorizontal ();
		
		GUILayout.EndArea ();
	}
	
	private void FancyTop ()
	{
		GUI.Label (new Rect ((Screen.width - 127) / 2, 24, 0, 0), "", "GoldLeaf");
		GUI.Label (new Rect ((Screen.width - 53) / 2, 24, 0, 0), "", "IconFrame");
		GUI.Label (new Rect ((Screen.width - 40) / 2, 34, 0, 0), "", "Skull");
	}
	
	private void DeathBadge ()
	{
		int x = 50;
		int y = Screen.height - 140;
		GUI.Label (new Rect (x - 6, y + 40, 0, 0), "", "RibbonRed");
		GUI.Label (new Rect (x, y + 6, 0, 0), "", "IconFrame");
		GUI.Label (new Rect (x + 7, y + 15, 0, 0), "", "Skull");
	}

	private void WaxSeal ()
	{
		int x = Screen.width - 115;
		int y = Screen.height - 140;
		GUI.Label (new Rect (x + 6, y + 40, 0, 0), "", "RibbonBlue");
		GUI.Label (new Rect (x, y, 0, 0), "", "WaxSeal");
	}
}
