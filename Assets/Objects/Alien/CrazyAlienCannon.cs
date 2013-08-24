using UnityEngine;
using System.Collections;

public class CrazyAlienCannon : MonoBehaviour {
	
	public GameObject laserPrefab;
	
	public float minWait;
	
	public float maxWait;
	
	public LayerMask layer;
	
	void Start () {
		StartCoroutine("RandomShoot");
	}
	
	IEnumerator RandomShoot() {
		
		while (gameObject.activeSelf) {
			yield return new WaitForSeconds(Random.Range(minWait, maxWait));
			
			if (!Physics.Raycast(transform.position, Vector3.down, layer)) {
				// Shoot!
				GameObject laser = (GameObject) GameObject.Instantiate(laserPrefab, transform.position, Quaternion.identity);
				Vector3 pos = laser.transform.position;
				pos.z = 400;
				laser.transform.position = pos;
				
				laser.GetComponent<ConstantSpeed>().speed.y = -80;
				laser.GetComponent<Bullet>().team = Team.Alien;
			}
			
		}
		
	}
}
