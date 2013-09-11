using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	
	public GameObject bulletPrefab;
	
	public AudioSource shootSound;
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			GameObject bullet = (GameObject) GameObject.Instantiate(bulletPrefab);
			
			bullet.GetComponent<Bullet>().team = Team.Player;
			bullet.GetComponent<ConstantSpeed>().speed = transform.forward * 5;
			
			Vector3 pos = transform.position + transform.forward;
			bullet.transform.position = pos;
			bullet.transform.forward = transform.forward;
			
			SoundManager.Instance.Play(shootSound);
        }
	}
}
