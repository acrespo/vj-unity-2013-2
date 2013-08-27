using UnityEngine;
using System.Collections;

public class MotherShipShootable : Shootable {

	public int score;

	public override void Hit () {
		renderer.enabled = false;
		transform.position = new Vector3(-110, 60, 400);
		ScoreKeeper.Instance.alienDied(score);
		SoundManager.Instance.Play(SoundManager.Instance.alienExplosion);
	}
}
