using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
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
	
	void Awake ()
	{
		life = maxLife;
		state = PlayerState.Normal;
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
		if (life <= 0) {
			state = PlayerState.Dying;
			audio.clip = deathSound;
			Debug.Log ("you are dead");
		} else {
			if (Random.Range (0, 2) == 0) {
				audio.clip = hitSound1;
			} else {
				audio.clip = hitSound2;
			}
		}
		if (!audio.isPlaying) {
			audio.Play ();
		}
	}
}
