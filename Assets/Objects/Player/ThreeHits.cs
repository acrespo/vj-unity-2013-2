using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(Cannon))]
public class ThreeHits : Shootable
{
	public override void Hit () {
		ScoreKeeper.Instance.playerDied();
		
		if (ScoreKeeper.Instance.Lives == -1) {
			togglePlayer(false);
			SoundManager.Instance.Play(SoundManager.Instance.playerExplosion);
		} else {
			SoundManager.Instance.Play(SoundManager.Instance.playerHit);
			StartCoroutine("die");
		}
	}
	
	void togglePlayer(bool state) {
		enabled = state;
		GetComponent<CharacterMotor>().enabled = state;
		GetComponent<Cannon>().enabled = state;		
	}
	
	public IEnumerator die() {
		transform.position = new Vector3(0, -60, 400);
		togglePlayer(false);
		
		for (int i = 0; i < 6; i++) {
			Vector3 pos = transform.position;
			pos.z = -pos.z;
			transform.position = pos;
			yield return new WaitForSeconds(0.15f);
		}
		
		yield return new WaitForSeconds(0.2f);
		togglePlayer(true);
	}
}

