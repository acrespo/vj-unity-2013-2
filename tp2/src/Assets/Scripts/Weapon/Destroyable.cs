using UnityEngine;
using System.Collections;

public class Destroyable : Shootable {
	
	public AudioSource sound;
	
	public delegate void OnDestroy(GameObject self);
	
	public OnDestroy onDestroy;
	
	public override void Hit () {
		
		ObjectPool.Instance.Return(gameObject);
		if (onDestroy != null) {
			onDestroy(gameObject);
		}
		
		if (sound) {
			SoundManager.Instance.Play(sound);
		}
	}
}
