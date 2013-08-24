using UnityEngine;
using System.Collections;

public class ThreeHits : Shootable
{
	public override void Hit () {
		ScoreKeeper.Instance.playerDied();
	}
}

