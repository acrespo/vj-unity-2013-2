using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MotherShipMovement))]
public class MotherShipShootable : Shootable {

	public int score;

	private MotherShipMovement movementManager;

	void Start () {
		movementManager = GetComponent<MotherShipMovement>();
	}

	public override void Hit () {
		movementManager.Disappear();
		ScoreKeeper.Instance.alienDied(score);
		SoundManager.Instance.Play(SoundManager.Instance.alienExplosion);
	}
}
