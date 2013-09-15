using UnityEngine;
using System.Collections;

public class PlayerCannon : Cannon {
	
	public PlayerCannon() {
		team = Team.Player;
	}
	
	
	void FixedUpdate () {
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			Shoot();
        }
	}

}
