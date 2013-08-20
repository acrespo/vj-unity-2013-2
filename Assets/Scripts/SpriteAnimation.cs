using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class SpriteAnimation : MonoBehaviour {
	
	public int frames;
	
	public float frameWidth;
	
	private int currentFrame = -1;
	
	// Use this for initialization
	void Start () {
		renderer.sharedMaterial.SetTextureScale("_MainTex", new Vector2(0.5f, 1.0f));
		Animate();
	}
	
	public void Animate () {
		
		currentFrame++;
		if (currentFrame >= frames) {
			currentFrame = 0;
			
		}

		Vector2 offset = new Vector2((float)currentFrame / (float)frames, 0);
		renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
