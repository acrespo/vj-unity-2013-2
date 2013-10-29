using UnityEngine;
using System.Collections.Generic;

public class RoomTrigger : MonoBehaviour {
	
	void OnTriggerEnter(Collider c) {
		
		if (c.gameObject.GetComponent<Player>() != null) {
			foreach (Transform t in transform.parent) {
				if (t.gameObject.GetComponent<Enemy>()) {
					t.gameObject.GetComponent<Enemy>().TriggerChase();
				} else if (t != transform) {
					t.gameObject.animation.Play("out-open-slowly");
				}
			}
		}
	}
}
