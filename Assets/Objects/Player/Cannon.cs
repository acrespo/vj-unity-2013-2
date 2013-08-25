using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	
	public GameObject laserPrefab;
	
	private GameObject laser;
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Space) && (!laser || !laser.activeSelf)) {
			laser = (GameObject) GameObject.Instantiate(laserPrefab);
			laser.GetComponent<Bullet>().team = Team.Player;
			
			Vector3 pos = transform.position + Vector3.up * 7;
			pos.z = 300;
			laser.transform.position = pos;
			
			SoundManager.Instance.Play(SoundManager.Instance.playerShoot);
        }
	}
}
