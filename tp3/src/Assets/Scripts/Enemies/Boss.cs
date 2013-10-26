using UnityEngine;
using System.Collections;

public class Boss : Enemy {

	protected override void PassedToAfterLife() {
		GameObject.Destroy(gameObject);
		World.Instance.LoadNextLevel();
	}
}
