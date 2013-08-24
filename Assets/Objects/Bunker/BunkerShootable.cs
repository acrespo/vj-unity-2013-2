using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteAnimation))]
public class BunkerShootable : Shootable {
	
	public int lives;
	
	private SpriteAnimation animator;

	void Start () {
		animator = GetComponent<SpriteAnimation>();
		
	}
	
	public override void Hit () {
		if (lives > 1) {
			animator.Animate();
			lives--;
		} else {
			GameObject.Destroy(gameObject);
		}
	}
}
