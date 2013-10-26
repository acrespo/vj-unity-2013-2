using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(Animation))]
public class Enemy : MonoBehaviour
{
	public enum EnemyState
	{
		Idle,
		Walking,
		Chasing,
		Attacking,
		WaitingForAttack,
		Dying
	}
	
	public Player player;
	public float speed = 5;
	public float gravity = 1;
	public float attackRange = 2;
	public float attackReloadTime = 1;
	public float maxLife = 100;
	public float damage = 10;
	public EnemyState initialState = EnemyState.Idle;
	
	// animation variables
	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip chaseAnimation;
	public AnimationClip attackAnimation;
	public AnimationClip attackAltAnimation;
	public AnimationClip dyingAnimation;
	public float idleAnimationSpeed = 1;
	public float walkAnimationSpeed = 1;
	public float chaseAnimationSpeed = 1;
	public float attackAnimationSpeed = 1;
	public float attackAltAnimationSpeed = 1;
	public float dyingAnimationSpeed = 1;
	public float applyDamageDelayRatio = 0.5f;
	
	// private variables
	private CharacterController controller;
	private Vector3 velocity = Vector3.zero;
	private EnemyState state;
	private float lastAttackTime;
	private float life;
	private bool damageApplied;
	private string choosenAttackAnimation;
	
	void Awake ()
	{
		controller = GetComponent<CharacterController> ();
		state = initialState;
		lastAttackTime = 0;
		life = maxLife;
		damageApplied = false;
		choosenAttackAnimation = "";
		
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
		// get the vector between the player and us
		Vector3 diff = new Vector3 (player.transform.position.x - transform.position.x, 0, 
			player.transform.position.z - transform.position.z);
		
		if (state == EnemyState.Idle) {
			velocity.x = 0;
			velocity.z = 0;
			animation.CrossFade (idleAnimation.name);
		} else if (state == EnemyState.Chasing) {
			// move towards the player
			Vector2 move = new Vector2 (diff.x, diff.z);
			move.Normalize ();
			move *= speed;
			velocity.x = move.x;
			velocity.z = move.y;
			animation.CrossFade (chaseAnimation.name);
			transform.LookAt (transform.position + diff);
			
			// change state if we reached attack range
			if (diff.sqrMagnitude <= attackRange * attackRange) {
				state = EnemyState.WaitingForAttack;
			}
			
			// change state if player is dead
			if (player.GetState () == Player.PlayerState.Dying) {
				state = EnemyState.Idle;
			}
		} else if (state == EnemyState.WaitingForAttack) {
			velocity.x = 0;
			velocity.z = 0;
			transform.LookAt (transform.position + diff);
			animation.CrossFade (idleAnimation.name);
			
			// change state if we got out of range
			if (diff.sqrMagnitude > attackRange * attackRange) {
				state = EnemyState.Chasing;
			}
			
			// change state if player is dead
			if (player.GetState () == Player.PlayerState.Dying) {
				state = EnemyState.Idle;
			}
			
			// start an attack if reload time is passed
			if (lastAttackTime + attackReloadTime <= Time.time) {
				lastAttackTime = Time.time;
				state = EnemyState.Attacking;
				damageApplied = false;
				
				// randomly use the alternate attack animation
				if (attackAltAnimation == null || Random.Range (0, 2) == 0) {
					choosenAttackAnimation = attackAnimation.name;
				} else {
					choosenAttackAnimation = attackAltAnimation.name;
				}
				animation.Play (choosenAttackAnimation);
			}
		} else if (state == EnemyState.Attacking) {
			velocity.x = 0;
			velocity.z = 0;
			transform.LookAt (transform.position + diff);
			
			// apply damage
			float applyDamageDelay = animation.GetClip (choosenAttackAnimation).length * applyDamageDelayRatio;
			if (!damageApplied && lastAttackTime + applyDamageDelay <= Time.time) {
				damageApplied = true;
				player.Damage (damage);
			}
			
			// change state if we finished this attack
			if (!animation.IsPlaying (choosenAttackAnimation)) {
				state = EnemyState.WaitingForAttack;
			}
		} else if (state == EnemyState.Dying) {
			velocity.x = 0;
			velocity.z = 0;
			
			// remove gameobject if we finished dying
			if (!animation.IsPlaying (dyingAnimation.name)) {
				PassedToAfterLife();
			}
		}
		
		// apply gravity
		velocity.y -= gravity * Time.deltaTime;
		velocity.y = Mathf.Max (velocity.y, -20);
		
		// apply the move
		controller.Move (velocity * Time.deltaTime);
	}
	
	public void TriggerChase()
	{
		state = EnemyState.Chasing;
	}
	protected virtual void PassedToAfterLife() {
		GameObject.Destroy (gameObject);
	}
	
	public void Damage (float amount)
	{
		life -= amount;
		if (life <= 0) {
			state = EnemyState.Dying;
			animation.Play (dyingAnimation.name);
		}
	}
}
