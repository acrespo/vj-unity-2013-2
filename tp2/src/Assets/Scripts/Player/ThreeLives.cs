using UnityEngine;
using System.Collections;

public class ThreeLives : Shootable {
	
	private World world;

	void Start () {
		world = transform.parent.GetComponent<World>();
	}
	
	public override void Hit() {
		
		GameObject explosion = ObjectPool.Instance.GetObject("Explosion");
		explosion.transform.position = gameObject.transform.position;
		explosion.GetComponent<Detonator>().Explode();
		SoundManager.Instance.Play(explosion.GetComponent<AudioSource>());
				
		if (world.PlayerLives > 0) {
			world.PlayerLives--;
		}	
	}
}
