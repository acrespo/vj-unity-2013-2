using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	
	public GameObject laserPrefab;
	
	private GameObject laser;

	// Use this for initialization
	void Start () {
		laser = (GameObject) GameObject.Instantiate(laserPrefab);
		laser.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Space) && !laser.activeSelf) {
            laser.SetActive(true);
			
			laser.transform.position = transform.position + Vector3.up * 7;
			laser.rigidbody.AddForce(Vector3.up * 10, ForceMode.VelocityChange);
        }
	}
}
