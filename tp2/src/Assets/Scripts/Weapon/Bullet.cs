using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public Team team;
	
	private float birth;
	
	void Start() {
		birth = Time.time;
	}
	
	void OnTriggerEnter(Collider collider) {
		Shootable imp = collider.gameObject.GetComponent<Shootable>();
		if (imp && imp.enabled && imp.team != team) {
			imp.Hit();
			GameObject.Destroy(gameObject);
		}
	}
	
	void FixedUpdate() {
		if (Time.time - birth > 100) {
			GameObject.Destroy(gameObject);
		}
	}
	
}
