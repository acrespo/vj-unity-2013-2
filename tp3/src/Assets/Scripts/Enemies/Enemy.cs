using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(Animation))]
public class Enemy : MonoBehaviour
{
	public enum EnemyState
	{
		Idle = 0,
		Walking = 1,
		Chasing = 2,
		Attacking = 3
	}
	
	public GameObject player;
	public float speed = 5;
	public float gravity = 20;
	public float attackRange = 2;
	public float attackReloadTime = 1;
	
	// animation variables
	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip chaseAnimation;
	public AnimationClip attackAnimation;
	public AnimationClip attackAltAnimation;
	public float idleAnimationSpeed = 1;
	public float walkAnimationSpeed = 1;
	public float chaseAnimationSpeed = 1;
	public float attackAnimationSpeed = 1;
	public float attackAltAnimationSpeed = 1;
	
	// private variables
	private CharacterController controller;
	private Vector3 moveDirection = Vector3.zero;
	private EnemyState state;
	private float lastAttackTime;
	
	void Awake ()
	{
		controller = GetComponent<CharacterController> ();
		state = EnemyState.Idle;
		lastAttackTime = 0;
		
		animation [idleAnimation.name].speed = idleAnimationSpeed;
		animation [walkAnimation.name].speed = walkAnimationSpeed;
		animation [chaseAnimation.name].speed = chaseAnimationSpeed;
		animation [attackAnimation.name].speed = attackAnimationSpeed;
		if (attackAltAnimation != null) {
			animation [attackAltAnimation.name].speed = attackAltAnimationSpeed;
		}
	}
	
	void Update ()
	{
		if (player != null) {
			// get the vector between the player and us
			Vector3 diff = new Vector3 (player.transform.position.x - transform.position.x, 0, 
					player.transform.position.z - transform.position.z);
			
			// attack if we are in range
			if (diff.sqrMagnitude < attackRange * attackRange) {
				state = EnemyState.Attacking;
				moveDirection.x = 0;
				moveDirection.z = 0;
				
				// play the idle animation if necessary
				if (animation.IsPlaying (walkAnimation.name) || animation.IsPlaying (chaseAnimation.name)) {
					animation.CrossFade (idleAnimation.name);
				}
				
				// if the reload time is passed
				if (lastAttackTime + attackReloadTime <= Time.time) {
					lastAttackTime = Time.time;
					
					// randomly use the alternate attack animation
					if (attackAltAnimation == null || Random.Range (0, 2) == 0) {
						animation.Play (attackAnimation.name);
					} else {
						animation.Play (attackAltAnimation.name);
					}
					animation.CrossFadeQueued (idleAnimation.name);
				}
			} else if (controller.isGrounded) {
				// else move towards the player
				state = EnemyState.Chasing;
				moveDirection.Set (diff.x, 0, diff.z);
				moveDirection.Normalize ();
				moveDirection *= speed;
			}
			
			// face the moving position
			transform.LookAt (transform.position + diff);
		}
		
		// apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// apply the move
		controller.Move (moveDirection * Time.deltaTime);
		
		// animations
		if (animation != null) {
			if (state == EnemyState.Idle) {
				animation.CrossFade (idleAnimation.name);
			} else if (state == EnemyState.Walking) {
				animation.CrossFade (walkAnimation.name);
			} else if (state == EnemyState.Chasing) {
				animation.CrossFade (chaseAnimation.name);
			}
		}
	}
}
