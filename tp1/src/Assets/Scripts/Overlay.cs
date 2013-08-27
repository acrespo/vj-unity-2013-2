using UnityEngine;
using System.Collections;

public class Overlay : MonoBehaviour {
	
	public GUISkin skin;
	
	public Texture playerTexture;
	
	private Texture2D overlayTexture;
	
	void Start() {
		overlayTexture = new Texture2D(1, 1);
		Color pixelColor = new Color(0f, 0f, 0f, 0.3f);
		overlayTexture.SetPixel(0, 0, pixelColor);
		overlayTexture.Apply();
	}
	
	void OnGUI() {
		GUI.Label(new Rect(10, 0, 100, 20), "Score:  " + ScoreKeeper.Instance.Score.ToString(), skin.label);
		
		int playerLives = ScoreKeeper.Instance.Lives;
		bool aliensReachedPlayer = ScoreKeeper.Instance.AliensReachedPlayer;
		bool gameOver = ScoreKeeper.Instance.AliensLeft == 0 || playerLives == -1 || aliensReachedPlayer;
		
		for (int i = 0; i < playerLives; i++) {
			
			Rect pos = new Rect(Screen.width - (playerTexture.width * 0.5f + 5.0f) * i - 20, 10, playerTexture.width * 0.5f, playerTexture.height * 0.5f);
			GUI.DrawTexture(pos, playerTexture, ScaleMode.ScaleToFit);
		}
		
		if (gameOver) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlayTexture);
			
			if (ScoreKeeper.Instance.AliensLeft == 0) {
				GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 200, 60), "YOU  WIN!", skin.customStyles[0]);
			}
			
			if (playerLives == -1) {
				GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 200, 60), "YOU   LOST :(", skin.customStyles[0]);	
			}
			
			Rect backRect = new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 60);
			Rect restartRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 + 40, 200, 60);
			
			if (GUI.Button(backRect, "Back to menu", skin.customStyles[1])) {
				Application.LoadLevel("Menu");
			}
			
			if (GUI.Button(restartRect, "Restart", skin.customStyles[1])) {
				Application.LoadLevel("Test");
			}
		}
		
	}
}
