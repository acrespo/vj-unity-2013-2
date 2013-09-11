using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public Team team;
	
	void OnTriggerEnter(Collider collider) {
		Debug.Log ("asdasd");
		Shootable imp = collider.gameObject.GetComponent<Shootable>();
		if (imp && imp.enabled && imp.team != team) {
			imp.Hit();
			GameObject.Destroy(gameObject);
		}
	}
	
}
