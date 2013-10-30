using UnityEngine;
using System.Collections;

public class Player : Singleton<Player>
{
	public enum PlayerState
	{
		Normal,
		Dying
	}
	
	public float maxLife = 100;
	public AudioClip hitSound1;
	public AudioClip hitSound2;
	public AudioClip deathSound;
	private PlayerState state;
	private float life;
	private Camera cam;
	private DamageFlash damageFlash;
	
	void Awake ()
	{
		life = maxLife;
		state = PlayerState.Normal;
		cam = Camera.main;
//		damageFlash = GameObject.FindGameObjectWithTag ("DamageFlash").GetComponent<DamageFlash> ();
		damageFlash = DamageFlash.Instance;
		
		bool useSound = PlayerPrefs.GetInt ("sound", 1) == 1;
		if (!useSound) {
			AudioListener.pause = true;
		}
	}
	
	void FixedUpdate ()
	{
		if (state == PlayerState.Dying) {
			if (Mathf.Abs (cam.transform.rotation.eulerAngles.z - 300) > 10) {
				cam.transform.Rotate (0, 0, -0.6f);
				cam.transform.Translate (0.02f, -0.015f, 0);
			} else {
				World.Instance.gameMenuManager.GameOver(false);
			}
		}
	}
	
	public PlayerState GetState ()
	{
		return state;
	}
	
	public float GetLife ()
	{
		return life;
	}
	
	public void Damage (float amount)
	{
		life -= amount;
		damageFlash.Flash ();
		if (life <= 0) {
			life = 0;
			Die ();
		} else {
			if (Random.Range (0, 2) == 0) {
				audio.clip = hitSound1;
			} else {
				audio.clip = hitSound2;
			}
			if (!audio.isPlaying) {
				audio.Play ();
			}
		}
	}
	
	public void Heal (float amount)
	{
		life += amount;
	}
	
	private void Die ()
	{
		state = PlayerState.Dying;
		audio.clip = deathSound;
		audio.Play ();
		GetComponent<MouseLook> ().enabled = false;
		cam.GetComponent<MouseLook> ().enabled = false;
		GameObject.Find ("Player/Graphics").renderer.enabled = false;
		GameObject.Find ("Player/Main Camera/Staff container").SetActive (false);
		GameObject.Find ("Player/Main Camera/Torch").SetActive (false);
		
	}
}
