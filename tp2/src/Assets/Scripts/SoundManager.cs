using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager>
{
	
	public void Play (AudioSource sound)
	{
		bool useSound = PlayerPrefs.GetInt ("sound", 1) == 1;
		if (useSound && sound != null) {
			sound.Play ();
		}
	}
}
