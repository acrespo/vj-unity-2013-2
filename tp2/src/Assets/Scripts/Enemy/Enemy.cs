using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	
	private Vector2 _spawnPoint;
	
	public Vector2 SpawnPoint {
		get {
			return _spawnPoint;
		}
		
		set {
			_spawnPoint = value;
		}
	}
}