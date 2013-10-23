using UnityEngine;
using System.Collections;

public class HealthBar2 : MonoBehaviour {

	public float health;
	public Texture2D texture1;
	public Material mat;
	public float x = 0;
	public float y = 0;
	public float w = 0;
	public float h = 0;
	

	void Start () {
		health = 100;
	}
	
	void OnGUI(){
		if(Event.current.type.Equals(EventType.Repaint)){ //Is the game being drawn?
		Rect box = new Rect(x, y, w, h);
			Graphics.DrawTexture(box, texture1, mat);
		}
	}

	
	void Update () {
		float healthy = 0.9f-(health/100);
		if(healthy <= 0){
			healthy = 0.1f;
		}
		mat.SetFloat("_Cutoff", healthy);
	}
}