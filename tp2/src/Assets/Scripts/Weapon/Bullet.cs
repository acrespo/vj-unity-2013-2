using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public Team team;
	
	void OnTriggerEnter(Collider collider) {
	
		Shootable imp = collider.gameObject.GetComponent<Shootable>();
		if (imp && imp.enabled && imp.team != team) {
			
			GameObject explosion = ObjectPool.Instance.GetObject("Explosion");
			explosion.transform.position = gameObject.transform.position;
			
			Detonator detonator = explosion.GetComponent<Detonator>();
			detonator.size = 3.0f;
			detonator.Explode();
		
			SoundManager.Instance.Play(explosion.GetComponent<AudioSource>());
		
			imp.Hit();
			ObjectPool.Instance.Return(gameObject);
		}
	}
}
