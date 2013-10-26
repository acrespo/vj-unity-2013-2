using UnityEngine;
using System.Collections;

public class StaffShooter : MonoBehaviour
{
	public const float RELOAD_TIME = 1;
	public GameObject fireball;
	public AnimationClip walkAnimation;
	public AnimationClip attackAnimation;
	private Camera cam;
	private float lastShootTime;
	
	void Start ()
	{
		cam = Camera.main.camera;
		lastShootTime = 0;
	}
	
	void FixedUpdate ()
	{
		if (Input.GetButton ("Fire1") && Time.time - lastShootTime >= RELOAD_TIME) {
			lastShootTime = Time.time;
			Vector3 pos = cam.transform.position + cam.transform.forward * 1;
			GameObject ball = (GameObject)Instantiate (fireball, pos, Quaternion.identity);
			ball.AddComponent<Light>();
			Light l = ball.light;
			l.type = LightType.Point;
			l.intensity = 1;
			l.color = Color.red + 0.2f * Color.yellow;
			l.range = 15;
			
			l.transform.parent = ball.transform;
			
			ball.rigidbody.AddForce (cam.transform.forward * 1400);
			ball.rigidbody.AddTorque (Random.Range (-25, 25), Random.Range (-25, 25), Random.Range (-25, 25));
			animation.Play (attackAnimation.name);
			animation.CrossFadeQueued (walkAnimation.name);
		}
	}
}
