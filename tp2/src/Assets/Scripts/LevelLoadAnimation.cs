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
	}
	
	
	public bool Update() {
		
		if (Time.realtimeSinceStartup >= end) {
			return false;
		}
		
		float delta = Time.realtimeSinceStartup - start;
		camera.position = Vector3.Slerp(origin, cameraDestination, delta / 3.0f);		
		
		return true;
	}
}

