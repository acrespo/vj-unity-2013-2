using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class BoxMover : MonoBehaviour {
	
	public float moveSpeed = 0.6f;
	
	[NonSerialized]
	public Vector3 moveDirection = Vector3.zero;
	
	void FixedUpdate () {
	
		Vector3 movementDelta = moveDirection * moveSpeed / Time.fixedDeltaTime;
		rigidbody.velocity = movementDelta;
		if (movementDelta.magnitude > 0) {
			rigidbody.rotation = Quaternion.LookRotation(movementDelta);
		}
	}
}
