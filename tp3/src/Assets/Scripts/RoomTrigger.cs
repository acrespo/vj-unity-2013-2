using UnityEngine;
using System.Collections.Generic;

public class RoomTrigger : MonoBehaviour {
	
	private bool alreadyTriggered = false;
		
	void OnTriggerEnter(Collider c) {
		
		if (c.gameObject.GetComponent<Player>() != null && !alreadyTriggered) {
			foreach (Transform t in transform.parent) {
				if (t.gameObject.GetComponent<Enemy>()) {
					alreadyTriggered = true;
					t.gameObject.GetComponent<Enemy>().TriggerChase();
				} else if (t != transform) {
					t.gameObject.animation.Play("out-open-slowly");
				}
			}
		}
	}
}
