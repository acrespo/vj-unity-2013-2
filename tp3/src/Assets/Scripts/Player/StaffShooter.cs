using UnityEngine;
using System.Collections;

public class StaffShooter : MonoBehaviour
{
	public const float RELOAD_TIME = 1;
	public GameObject fireball;
	private Camera cam;
	private float lastShootTime;
	
	void Start ()
	{
		cam = Camera.main.camera;
		lastShootTime = 0;
	}
	
	void FixedUpdate ()
	{
		Debug.DrawRay (cam.transform.position, cam.transform.forward * 1);
		if (Input.GetButton ("Fire1") && Time.time - lastShootTime >= RELOAD_TIME) {
			lastShootTime = Time.time;
			Vector3 pos = cam.transform.position + cam.transform.forward * 1;
			GameObject ball = (GameObject)Instantiate (fireball, pos, Quaternion.identity);
			ball.rigidbody.AddForce (cam.transform.forward * 1400);
		}
	}
}
