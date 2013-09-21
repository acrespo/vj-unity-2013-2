using System;
using UnityEngine;

public class LevelLoadAnimation
{
	private Vector3 cameraDestination;
	
	private Vector3 origin;
	
	private Transform camera;
	
	private float start;
	
	private float end;
	
	private bool done;
	
	public LevelLoadAnimation (Vector3 cameraDestination)
	{
		done = false;
		this.cameraDestination = cameraDestination;
		start = Time.realtimeSinceStartup;
		end = start + 3.0f;
		camera = Camera.main.transform;
		origin = camera.transform.position;
		Debug.Log (cameraDestination);
	}
	
	
	public bool Update() {
		
		if (Time.realtimeSinceStartup >= end) {
			Debug.Log ("Hello!");
			return false;
		}
		
		float delta = Time.realtimeSinceStartup - start;
		Debug.Log (delta);
		camera.position = Vector3.Slerp(origin, cameraDestination, delta / 3.0f);		
		
		return true;
	}
}

