using System;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxMover))]
[RequireComponent(typeof(Cannon))]
public class Enemy : MonoBehaviour
{
	
	public float timeBetweenMovesLow = 1.0f;
	public float timeBetweenMovesHigh = 3.0f;
	public float rayLenNear = 0.6f;
	public float rayLenFar = 1.2f;
	public float raySpace = 0.2f;
	
	public float firingDistance = 9.0f;
	public float shootChance = 0.7f;
	
	private float nextMoveTime = 0f;
	private Vector3 rayPos1;
	private Vector3 rayPos2;
	
	private bool shooting;
	
	private Vector2 _spawnPoint;
	
	private Cannon cannon;
	private BoxMover mover;
	
	public Vector2 SpawnPoint {
		get {
			return _spawnPoint;
		}
		
		set {
			_spawnPoint = value;
		}
	}
	
	void Start ()
	{
		mover = GetComponent<BoxMover> ();
		cannon = GetComponent<Cannon>();
		nextMoveTime = Time.time + timeBetweenMovesLow;
		rayPos1 = new Vector3 ();
		rayPos2 = new Vector3 ();
	}
	
	void FixedUpdate ()
	{
		// check if we are gonna collide something
		bool gonnaCollide = false;
		if (mover.moveDirection.sqrMagnitude > 0) {
			// get the 2 raycasts position
			if (mover.moveDirection.z != 0) {
				rayPos1.Set (transform.position.x - raySpace, transform.position.y + 0.1f, transform.position.z);
				rayPos2.Set (transform.position.x + raySpace, transform.position.y + 0.1f, transform.position.z);
			} else {
				rayPos1.Set (transform.position.x, transform.position.y + 0.1f, transform.position.z - raySpace);
				rayPos2.Set (transform.position.x, transform.position.y + 0.1f, transform.position.z + raySpace);
			}
			
			// for debug
			Debug.DrawRay (rayPos1, mover.moveDirection.normalized * rayLenNear);
			Debug.DrawRay (rayPos2, mover.moveDirection.normalized * rayLenNear);
			
			
			if (Physics.Raycast (rayPos1, mover.moveDirection, rayLenNear) || Physics.Raycast (rayPos2, mover.moveDirection, rayLenNear)) {
				// if the raycasts hit
				gonnaCollide = true;
			}
		}
		Debug.DrawRay(transform.position + Vector3.up * 0.1f, mover.moveDirection * firingDistance);
		
		// if it's time to change direction or we are gonna collide something
		if (Time.time > nextMoveTime || (!shooting && gonnaCollide)) {
			
			bool shoot = false;
			RaycastHit hit;
			if (Physics.Raycast(transform.position + Vector3.up * 0.1f, mover.moveDirection, out hit, firingDistance)) {
				
				Shootable shootable = hit.transform.gameObject.GetComponent<Shootable>();
				if (shootable) {
					
					if (shootable.team == Team.Player) {
						shoot = true;
					} else if (shootable.team != Team.Enemy) {
						shoot = UnityEngine.Random.value > shootChance;
					}
				}

			}
			
			if (shoot) {
				// SHOOOOOOT
				cannon.Shoot();
				mover.moveDirection = Vector3.zero;
				nextMoveTime = Time.time + timeBetweenMovesLow;
				shooting = true;
			} else {
				shooting = false;
				ChangeDirection ();
			}
		}
	}
	
	void ChangeDirection ()
	{
		List<Vector3> validDirections = ValidDirections ();
		
		// pick a random direction in the valid directions
		int choice = UnityEngine.Random.Range (0, validDirections.Count);
		
		// if it's the opposite as our last direction, pick another one
		if (Vector3.Dot (mover.moveDirection, validDirections [choice]) < 0) {
			Debug.Log ("oposite");
			choice = (choice + 1) % validDirections.Count;
		}
		
		//Vector3 direction = new Vector3(choice % 2 == 0 ? choice - 1 : 0, 0, choice % 2 == 1 ? choice - 2 : 0);
		Vector3 direction = validDirections [choice];
		direction /= direction.magnitude * 2;
		
		mover.moveDirection = direction;
		
		nextMoveTime = Time.time + UnityEngine.Random.Range (timeBetweenMovesLow, timeBetweenMovesHigh);
	}
	
	List<Vector3> ValidDirections ()
	{
		List<Vector3> directions = new List<Vector3> ();
		
		// forward and backwards raycast position
		rayPos1.Set (transform.position.x - raySpace, transform.position.y + 0.1f, transform.position.z);
		rayPos2.Set (transform.position.x + raySpace, transform.position.y + 0.1f, transform.position.z);
		
		// forward
		if (!Physics.Raycast (rayPos1, Vector3.forward, rayLenFar) && !Physics.Raycast (rayPos2, Vector3.forward, rayLenFar)) {
			directions.Add (Vector3.forward);
		}
		
		// backwards
		if (!Physics.Raycast (rayPos1, Vector3.back, rayLenFar) && !Physics.Raycast (rayPos2, Vector3.back, rayLenFar)) {
			directions.Add (Vector3.back);
		}
		
		// left and right raycast position
		rayPos1.Set (transform.position.x, transform.position.y + 0.1f, transform.position.z - raySpace);
		rayPos2.Set (transform.position.x, transform.position.y + 0.1f, transform.position.z + raySpace);
		
		// left
		if (!Physics.Raycast (rayPos1, Vector3.left, rayLenFar) && !Physics.Raycast (rayPos2, Vector3.left, rayLenFar)) {
			directions.Add (Vector3.left);
		}
		
		// right
		if (!Physics.Raycast (rayPos1, Vector3.right, rayLenFar) && !Physics.Raycast (rayPos2, Vector3.right, rayLenFar)) {
			directions.Add (Vector3.right);
		}
		
		return directions;
	}
}