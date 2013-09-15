using System;
using UnityEngine;

[RequireComponent(typeof(BoxMover))]
[RequireComponent(typeof(Cannon))]
public class Enemy : MonoBehaviour
{
	
	public float timeBetweenMoves = 1.0f;
	
	private float nextMoveTime = 0f;
	
	private Vector2 _spawnPoint;
	
	private BoxMover mover;
	
	public Vector2 SpawnPoint {
		get {
			return _spawnPoint;
		}
		
		set {
			_spawnPoint = value;
		}
	}
	
	void Start() {
		mover = GetComponent<BoxMover>();
		nextMoveTime = Time.time + timeBetweenMoves;
	}
	
	void FixedUpdate() {
		
		if (Time.time > nextMoveTime) {
			
			int choice = UnityEngine.Random.Range (0, 4);
			
			Vector3 direction = new Vector3(choice % 2 == 0 ? choice - 1 : 0, 0, choice % 2 == 1 ? choice - 2 : 0);
			direction /= direction.magnitude * 2;
			
			mover.moveDirection = direction;
			
			nextMoveTime = Time.time + timeBetweenMoves;
		}
	}
}