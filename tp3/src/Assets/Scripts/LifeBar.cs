using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {

	public Camera GUICam;
	
	void Update() {
		//This may be done a little better...
		float width = 150;
		float height = 200;
		transform.position = GUICam.ScreenToWorldPoint(new Vector3(0+width/2f,GUICam.pixelHeight-height/2f, 1.25f*Screen.width/604f));
		renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x)); 
	}
}

