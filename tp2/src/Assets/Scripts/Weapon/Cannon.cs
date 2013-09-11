using UnityEngine;
using System.Collections;

public abstract class Cannon : MonoBehaviour {
	
	public GameObject bulletPrefab;
	
	protected Team team;
	
	public AudioSource shootSound;
	
	protected void Shoot() {
		GameObject bullet = (GameObject) GameObject.Instantiate(bulletPrefab);
		
		bullet.GetComponent<Bullet>().team = team;
		
		Vector3 speed =  transform.forward * 10;
		speed.y = 0;
		bullet.GetComponent<ConstantSpeed>().speed = speed;
		
		Vector3 pos = transform.position + transform.forward;
		bullet.transform.position = pos;
		bullet.transform.forward = transform.forward;
		bullet.transform.parent = transform.parent;
		
		SoundManager.Instance.Play(shootSound);
	}
}
