using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SpriteAnimation))]
public class AlienMovement : MonoBehaviour {
	
	private CharacterController controller;
	
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
		controller = GetComponent<CharacterController>();
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
		
		controller.Move(move); 
	}
	
}
