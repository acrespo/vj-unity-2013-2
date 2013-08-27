using UnityEngine;
using System.Collections;

public class ScoreKeeper : Singleton<ScoreKeeper> {
	
	private int score = 0;
	
	private int aliens = 0;
	
	private int lives = 3;
	
	private bool aliensReachPlayer = false;

	public void Reset() {
		score = 0;
		aliens = 0;
		lives = 3;
	}
	
	public int Score {
		get {
			return score;
		}
	}
	
	public int Lives {
		get {
			return lives;
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
	
	public void playerDied() {
		lives--;
	}

	public bool AliensReachedPlayer {
		get {
			return aliensReachPlayer;
		}
		set {
			aliensReachPlayer = true;
		}
	}
}
