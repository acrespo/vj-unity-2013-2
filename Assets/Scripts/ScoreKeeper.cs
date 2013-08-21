using UnityEngine;
using System.Collections;

public class ScoreKeeper : Singleton<ScoreKeeper> {
	
	private int score = 0;
	
	public int Score {
		get {
			return score;
		}
	}

	public void addScore(int score) {
		this.score += score;
	}
}
