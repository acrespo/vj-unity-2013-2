using UnityEngine;
using System.Collections;

public class ThreeHits : Shootable
{
	public override void Hit () {
		ScoreKeeper.Instance.playerDied();
		
		if (ScoreKeeper.Instance.Lives == 0) {
			enabled = false;
			GetComponent<CharacterMotor>().enabled = false;
			GetComponent<Cannon>().enabled = false;
			SoundManager.Instance.Play(SoundManager.Instance.playerExplosion);
		}else{
			SoundManager.Instance.Play(SoundManager.Instance.playerHit);
		}
	}
}

