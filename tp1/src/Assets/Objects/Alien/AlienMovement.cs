using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteAnimation))]
public class AlienMovement : MonoBehaviour {
	
	private SpriteAnimation animator;
	
	public float movementBeforeTurn = 30;
	
	public float delta = 1;
	
	public float drop = 3;
	
	private float movementInDirection = 0;
	
	private float direction = 1;
	
	private int updatesElapsed = 0;
	
	public int updatesTillMove = 3;

	// Use this for initialization
	void Start () {
		animator = GetComponent<SpriteAnimation>();
		
		// Start in the center of the movement area
		movementInDirection = movementBeforeTurn / 2;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (updatesElapsed < updatesTillMove) {
			updatesElapsed++;
			return;
		} else {
			updatesElapsed = 0;
		}
		
		animator.Animate();
		movementInDirection += delta;
		
		Vector3 move = new Vector3(delta * direction, 0, 0);
		
		if (movementInDirection > movementBeforeTurn) {
			direction = -direction;
			movementInDirection = 0;
			
			// Disable movement in x so that the jump looks less crappy
			move.y = -drop;
			move.x = 0;
		}
		
		transform.position += move;
		
		AudioSource invader = SoundManager.Instance.invader;
		if(!invader.isPlaying){
			SoundManager.Instance.Play(invader);
			invader.pitch -= 0.1f;
			if (invader.pitch < 0.65f) {
				invader.pitch = 1.0f;
			}
		}

		if (transform.position.y <= -60 ) {
			ScoreKeeper.Instance.AliensReachedPlayer = true;
		}
	}
	
}
