using UnityEngine;
using System.Collections;

public class ScoreKeeper : Singleton<ScoreKeeper> {
	
	private int score = 0;
	
	private int aliens = 0;
	
	public int Score {
		get {
			return score;
		}
	}
	
	public int AliensLeft {
		get {
			return aliens;
		}
		set {
			if (value >= 0) {
				aliens = value;
			}
		}
	}
	
	public void alienDied(int score) {
		this.score += +score;
		aliens--;
	}
	
}
