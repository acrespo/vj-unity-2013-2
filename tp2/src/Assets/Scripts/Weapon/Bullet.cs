using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public Team team;
	
	void OnTriggerEnter(Collider collider) {
		Shootable imp = collider.gameObject.GetComponent<Shootable>();
		if (imp && imp.enabled && imp.team != team) {
			imp.Hit();
			ObjectPool.Instance.Return(gameObject);
		}
	}
}
