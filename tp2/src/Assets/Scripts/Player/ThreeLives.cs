using UnityEngine;
using System.Collections;

public class ThreeLives : Shootable {
	
	private World world;

	void Start () {
		world = transform.parent.GetComponent<World>();
	}
	
	public override void Hit() {
		
		if (world.PlayerLives > 0) {
			world.PlayerLives--;
		}	
	}
}
