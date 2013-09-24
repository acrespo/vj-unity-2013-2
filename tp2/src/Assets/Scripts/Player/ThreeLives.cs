using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class ThreeLives : Shootable {
	
	private World world;
	private PlayerController playerController;
	
	void Start () {
		world = transform.parent.GetComponent<World>();
		playerController = gameObject.GetComponent<PlayerController>();
	}
	
	public override void Hit() {
		
		if (world.PlayerLives >= 0) {
			world.PlayerLives--;
			Vector2 point = playerController.SpawnPoint;
			gameObject.transform.position = new Vector3(point.y, 0f, point.x);
		}
	}
}
