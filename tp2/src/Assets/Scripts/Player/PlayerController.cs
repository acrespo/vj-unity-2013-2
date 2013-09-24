using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BoxMover))]
public class PlayerController : MonoBehaviour {
	
	private BoxMover mover;
	private Vector2 _spawnPoint;
	
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
	}
	
	void FixedUpdate () {
		
		float horiz = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");
		
		Vector3 dir;
		if (Math.Abs(horiz) > Math.Abs(vert)) {
			dir = new Vector3(horiz, 0, 0);
		} else {
			dir = new Vector3(0, 0, vert);
		}
		
		mover.moveDirection = dir;
		
	}
}
