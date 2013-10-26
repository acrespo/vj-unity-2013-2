using UnityEngine;
using System.Collections.Generic;

public class RoomTrigger : MonoBehaviour {
	
	void OnTriggerEnter(Collider c) {
		
		if (c.gameObject.GetComponent<Player>() != null) {
			foreach (Transform t in transform.parent) {
				if (t != transform) {
					t.gameObject.GetComponent<Enemy>().TriggerChase();
				}
			}
		}
	}
}
