using UnityEngine;
using System.Collections;

public class Overlay : MonoBehaviour {
	
	public GUISkin skin;
	
	void OnGUI() {
		GUI.Label(new Rect(10, 0, 100, 20), "Score:  " + ScoreKeeper.Instance.Score.ToString(), skin.label);
	}
}
