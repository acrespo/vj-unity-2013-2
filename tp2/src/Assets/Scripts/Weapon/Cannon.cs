using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	
	protected Team team;
	
	public AudioSource shootSound;
	
	public void Shoot() {
		GameObject bullet = ObjectPool.Instance.GetObject("Bullet");
		
		bullet.GetComponent<Bullet>().team = team;
		
		Vector3 speed =  transform.forward * 10;
		speed.y = 0;
		bullet.GetComponent<ConstantSpeed>().speed = speed;
		
		Vector3 pos = transform.position + 2 * transform.forward;
		bullet.transform.position = pos;
		bullet.transform.forward = transform.forward;
		bullet.transform.parent = transform.parent;
		
		SoundManager.Instance.Play(shootSound);
	}
}
