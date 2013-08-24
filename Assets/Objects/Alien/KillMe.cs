using UnityEngine;
using System.Collections;

public class KillMe : Shootable {
	
	public int score;
	
	public override void Hit () {
		GameObject.Destroy(gameObject);
		ScoreKeeper.Instance.alienDied(score);
	}
}
