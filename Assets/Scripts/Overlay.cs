using UnityEngine;
using System.Collections;

public class Overlay : MonoBehaviour {
	
	public GUISkin skin;
	
	void OnGUI() {
		GUI.Label(new Rect(10, 0, 100, 20), "Score:  " + ScoreKeeper.Instance.Score.ToString(), skin.label);
		
		if (ScoreKeeper.Instance.AliensLeft == 0) {
			GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), "YOU WIN!", skin.customStyles[0]);
		}
	}
}
