using UnityEngine;
using System.Collections;

public class PlayerCannon : Cannon {
	
	void Awake() {
		team = Team.Player;
	}
	
	void FixedUpdate () {
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			Shoot();
        }
	}

}
