using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {
		
	void Update() {
		renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x)); 
	}
}

