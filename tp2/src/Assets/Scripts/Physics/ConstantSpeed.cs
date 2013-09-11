using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ConstantSpeed : MonoBehaviour {

	public Vector3 speed;
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = speed;
	}
}
