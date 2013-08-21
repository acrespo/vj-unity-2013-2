using UnityEngine;
using System.Collections;

public class KillMe : MonoBehaviour {
	
	void OnTriggerEnter (Collider collider) {
		GameObject.Destroy(gameObject);
	}
}
