using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {
	
	private MeshFilter filter;
	
	public int frames;
	
	public float frameWidth;
	
	private int currentFrame = -1;
	
	// Use this for initialization
	void Start () {
		filter = GetComponent<MeshFilter>();
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
