using UnityEngine;
using System.Collections;

public class DamageFlash : Singleton<DamageFlash>
{
	public float fadeSpeed = 5;
	private string state;
	
	void Awake ()
	{
		//guiTexture.pixelInset = new Rect (0, 0, Screen.width, Screen.height);
		guiTexture.color = Color.clear;
		guiTexture.enabled = false;
		state = "none";
	}
	
	void Update ()
	{
		if (state == "fadeIn") {
			FadeIn ();
			if (guiTexture.color.a >= 0.5f) {
				state = "fadeOut";
			}
		} else if (state == "fadeOut") {
			FadeOut ();
			if (guiTexture.color.a <= 0.05f) {
				guiTexture.color = Color.clear;
				guiTexture.enabled = false;
				state = "none";
			}
		}
	}
	
	void FadeIn ()
	{
		guiTexture.color = Color.Lerp (guiTexture.color, Color.red, fadeSpeed * Time.deltaTime);
	}
	
	void FadeOut ()
	{
		guiTexture.color = Color.Lerp (guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	public void Flash ()
	{
		state = "fadeIn";
		guiTexture.enabled = true;
	}
}
