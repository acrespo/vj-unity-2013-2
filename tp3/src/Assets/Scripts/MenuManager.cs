using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public GUISkin skin;
	public Texture2D background;
	public int spikesY;
	public int titleX;
	public int titleY;
	private string state = "main";
	public float baseX = 50;
	public float mainY ;
	public int fancyTopY;
	public int titleHeight = 50;
	public int titleFontSize;
	public int fancyTopOffsetX;
	public int spikesWidth;
	public int deathBadgeX;
	public int deathBadgeY;
	public int waxSealX;
	public int waxSealY;
	private float outsideLeftX;
	private float outsideRightX;
	public float mainX;
	private float otherX;
	private Rect mainWindowRect;
	
	void Start() {
		outsideLeftX = -Screen.width;
		outsideRightX = Screen.width*2;
		mainX = baseX;
		otherX = baseX;
		spikesY = 10;
		titleX = 20;
		titleY = 50;
		mainY = 60;
		fancyTopY = -3;
		titleFontSize = 34;
		fancyTopOffsetX = 120;
		spikesWidth = 620;
		deathBadgeX = 10;
		deathBadgeY = 140;
		waxSealX = 570;
		waxSealY = 250;
	}
	
	void OnGUI () {
		GUI.skin = skin;
		
		mainWindowRect = GUI.Window (0, new Rect (0, 20, Screen.width, Screen.height-20), ShowMain, "");
//		GUI.DrawTexture(mainWindowRect, background);
		
		int prevFontSize = skin.label.fontSize;
		skin.label.fontSize = titleFontSize;
		GUILayout.BeginArea(new Rect(0,0, Screen.width, titleHeight));
		GUILayout.Label("They are taking the hobbits to Isengard!", GUILayout.Height(50));
		GUILayout.EndArea();
		skin.label.fontSize = prevFontSize;
		switch (state) {
		case "main":
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowMain, "");
			break;
		case "options":
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowOptions, "");
			break;
		case "credits":
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowCredits, "");
			break;
		case "transitionOptions":
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowMain, "");
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowOptions, "");
			mainX = Mathf.SmoothStep(mainX, outsideLeftX, Time.deltaTime * 10);
			otherX = Mathf.SmoothStep(otherX, baseX, Time.deltaTime * 10);
			if (mainX - outsideLeftX < 0.1f && otherX - baseX < 0.1f) {
				state = "options";
			}
			break;
		case "transitionOptionsBack":
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowMain, "");
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowOptions, "");
			mainX = Mathf.SmoothStep(mainX, baseX, Time.deltaTime * 10);
			otherX = Mathf.SmoothStep(otherX, outsideRightX, Time.deltaTime * 10);
			if (baseX - mainX < 0.1f && outsideRightX - otherX < 0.1f) {
				state = "main";
			}
			break;
		case "transitionCredits":
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowMain, "");
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowCredits, "");
			mainX = Mathf.SmoothStep(mainX, outsideLeftX, Time.deltaTime * 10);
			otherX = Mathf.SmoothStep(otherX, baseX, Time.deltaTime * 10);
			if (mainX - outsideLeftX < 0.1f && otherX - baseX < 0.1f) {
				state = "credits";
			}
			break;
		case "transitionCreditsBack":
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowMain, "");
			mainWindowRect = GUI.Window(0, mainWindowRect, ShowCredits, "");
			mainX = Mathf.SmoothStep(mainX, baseX, Time.deltaTime * 10);
			otherX = Mathf.SmoothStep(otherX, outsideRightX, Time.deltaTime * 10);
			if (baseX - mainX < 0.1f && outsideRightX - otherX < 0.1f) {
				state = "main";
			}
			break;
		}
	}
	
	private void ShowMain(int windowId)
	{
		GUILayout.BeginArea(new Rect(mainX, mainY, Screen.width, Screen.height));
	
		AddSpikes(spikesWidth);
		DeathBadge(deathBadgeX,deathBadgeY);
		FancyTop(Screen.width);
		WaxSeal(waxSealX, waxSealY);
		
		GUILayout.BeginArea(new Rect(mainX, mainY, 400, 400));
		if (GUILayout.Button("Start game", "button")) {
			Application.LoadLevel(1);
		}
		if (GUILayout.Button("Options", "button")) {
			state = "transitionOptions";
			mainX = baseX;
			otherX = outsideRightX;
		}
		if (GUILayout.Button("Credits", "button")) {
			state = "transitionCredits";
			mainX = baseX;
			otherX = outsideRightX;
		}
		GUILayout.EndArea();
		GUILayout.EndArea();
	}
	
	private void ShowOptions(int windowId) {
		GUILayout.BeginArea (new Rect (otherX, mainY+mainY/2, 400, 400));
		
		GUILayout.Label("Options");
		
		bool useSound = PlayerPrefs.GetInt("sound", 1) == 1;
		useSound = GUILayout.Toggle (useSound, "  Sound effects");
		PlayerPrefs.SetInt("sound", useSound ? 1 : 0);
		
		if (GUILayout.Button ("Back", "button")) {
			state = "transitionOptionsBack";
			mainX = outsideLeftX;
			otherX = baseX;
		}
		
		GUILayout.EndArea();
	}
	
	private void ShowCredits(int windowId) {
		GUILayout.BeginArea(new Rect (otherX, mainY+mainY/2, 400, 400));
		
		GUILayout.Label("Credits");
		
		GUILayout.Button("Juan", "buttonLabelStyle");
		GUILayout.Button("Alvaro", "buttonLabelStyle");
		GUILayout.Button("Frederic", "buttonLabelStyle");
		
		if (GUILayout.Button("Back", "button")) {
			state = "transitionCreditsBack";
			mainX = outsideLeftX;
			otherX = baseX;
		}
		
		GUILayout.EndArea();
	}
	
	private void AddSpikes(int winX) {
		
		float spikeCount = Mathf.Floor(winX - 152)/22;
		GUILayout.BeginHorizontal();
		GUILayout.Label("", "SpikeLeft");
		for (int i = 0; i < spikeCount; i++) {
			GUILayout.Label("", "SpikeMid");
	    }
		GUILayout.Label("", "SpikeRight");
		GUILayout.EndHorizontal();
	}
	
	
	private void DeathBadge(int x, int y) {
		int RibbonOffsetX = x;
		int FrameOffsetX = x+3;
		int SkullOffsetX = x+10;
		int RibbonOffsetY = y+22;
		int FrameOffsetY = y;
		int SkullOffsetY = y+9;
		
		GUI.Label(new Rect(RibbonOffsetX, RibbonOffsetY, 0, 0), "", "RibbonRed");//-------------------------------- custom	
		GUI.Label(new Rect(FrameOffsetX, FrameOffsetY, 0, 0), "", "IconFrame");//-------------------------------- custom	
		GUI.Label(new Rect(SkullOffsetX, SkullOffsetY, 0, 0), "", "Skull");//-------------------------------- custom	
	}
	
	
	private void FancyTop(int topX){
		int leafOffset = (topX/2)-fancyTopOffsetX;
		int frameOffset = (topX/2)-fancyTopOffsetX+34;
		int skullOffset = (topX/2)-fancyTopOffsetX+41;;
		GUI.Label(new Rect(leafOffset, fancyTopY+15, 0, 0), "", "GoldLeaf");
		GUI.Label(new Rect(frameOffset, fancyTopY, 0, 0), "", "IconFrame");
		GUI.Label(new Rect(skullOffset, fancyTopY+9, 0, 0), "", "Skull");
	}

	private void WaxSeal(int x, int y) {
		int WSwaxOffsetX = x - 120;
		int WSwaxOffsetY = y - 115;
		int WSribbonOffsetX = x - 114;
		int WSribbonOffsetY = y - 83;
		
		GUI.Label(new Rect(WSribbonOffsetX, WSribbonOffsetY, 0, 0), "", "RibbonBlue");//-------------------------------- custom	
		GUI.Label(new Rect(WSwaxOffsetX, WSwaxOffsetY, 0, 0), "", "WaxSeal");//-------------------------------- custom	
	}
}
