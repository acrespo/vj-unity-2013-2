using UnityEngine;
using System.Collections;

public class Stopper : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
		gameObject.SetActive(false);
	}
}
