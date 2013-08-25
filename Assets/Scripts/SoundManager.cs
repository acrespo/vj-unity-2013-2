using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager>
{
	public AudioSource alienExplosion;
	public AudioSource hit;
	public AudioSource invader;
	public AudioSource playerExplosion;
	public AudioSource playerShoot;
	public AudioSource ufo;
	public AudioSource playerHit;
	
	public void Play (AudioSource sound)
	{
		bool useSound = PlayerPrefs.GetInt ("sound", 1) == 1;
		if (useSound) {
			sound.Play ();
		}
	}
}
