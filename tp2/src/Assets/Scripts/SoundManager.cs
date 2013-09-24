using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager>
{
	
	public void Play (AudioSource sound)
	{
		bool useSound = PlayerPrefs.GetInt ("sound", 1) == 1;
		
		Debug.Log ("shoot sound" + useSound);
		if (useSound && sound != null) {
			Debug.Log ("shoot sound");
			sound.Play ();
		}
	}
}
