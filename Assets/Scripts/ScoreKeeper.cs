using UnityEngine;
using System.Collections;

public class ScoreKeeper : Singleton<ScoreKeeper> {
	
	private int score = 0;

	void OnGUI() {
		GUI.Label(new Rect(10, 0, 100, 20), score.ToString());
	}
	
	public void addScore(int score) {
		this.score += score;
	}
}
