using UnityEngine;
using System.Collections;

public class KillMe : MonoBehaviour {
	
	public int score;
	
	void OnTriggerEnter (Collider collider) {
		GameObject.Destroy(gameObject);
		ScoreKeeper.Instance.addScore(score);
	}
}
