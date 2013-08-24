using UnityEngine;
using System.Collections;

public class Overlay : MonoBehaviour {
	
	public GUISkin skin;
	
	public Texture playerTexture;
	
	void OnGUI() {
		GUI.Label(new Rect(10, 0, 100, 20), "Score:  " + ScoreKeeper.Instance.Score.ToString(), skin.label);
		
		if (ScoreKeeper.Instance.AliensLeft == 0) {
			GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), "YOU  WIN!", skin.customStyles[0]);
		}
		
		int playerLives = ScoreKeeper.Instance.Lives;
		if (playerLives == 0) {
			GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), "YOU   LOST :(", skin.customStyles[0]);
		}
		
		for (int i = 0; i < playerLives; i++) {
			
			Rect pos = new Rect(Screen.width - (playerTexture.width * 0.5f + 5.0f) * i - 20, 10, playerTexture.width * 0.5f, playerTexture.height * 0.5f);
			GUI.DrawTexture(pos, playerTexture, ScaleMode.ScaleToFit);
		}
	}
}
