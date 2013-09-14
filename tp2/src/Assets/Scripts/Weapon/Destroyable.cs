using UnityEngine;
using System.Collections;

public class Destroyable : Shootable {
	
	public AudioSource sound;
	
	public override void Hit () {
		
		ObjectPool.Instance.Return(gameObject);
		
		if (sound) {
			SoundManager.Instance.Play(sound);
		}
	}
}
