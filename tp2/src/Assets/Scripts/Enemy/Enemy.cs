using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	
	public float timeBetweenMoves = 1.0f;
	
	private float lastMoveTime = 0f;
	
	private Vector2 _spawnPoint;
	
	public Vector2 SpawnPoint {
		get {
			return _spawnPoint;
		}
		
		set {
			_spawnPoint = value;
		}
	}
	
	void FixedUpdate() {
		
		if (Time.time > lastMoveTime + timeBetweenMoves) {
			Vector3 direction = new Vector3(UnityEngine.Random.Range(-1, 2), 1, UnityEngine.Random.Range(-1, 2));
			direction /= direction.magnitude * 2;
			
			GetComponent<CharacterMotor>().inputMoveDirection = direction;
			
			lastMoveTime = Time.time;
		}
	}
}