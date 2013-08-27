using UnityEngine;
using System.Collections;

public class MotherShipMovement : MonoBehaviour {
	
	public float delta = 1;
		
	private float direction = 1;
	
	private int updatesElapsed = 0;
	
	public int updatesTillMove = 1;
	
	public float chance = 0.03f;

	public int height = 80;

	// Use this for initialization
	void Start () {
		StartCoroutine("MotherShipAppearance");
	}
	
	IEnumerator MotherShipAppearance() {
		while (gameObject.activeSelf) {
			yield return new WaitForSeconds(1);
			
			if (Random.value > chance) {
				continue;
			}

			if (!renderer.enabled) {
				if (Random.value >= 0.5) {
					direction = 1;
					transform.position = new Vector3(-84, height, 400);
				} else {
					direction = -1;
					transform.position = new Vector3(84, height, 400);
				}
				renderer.enabled = true;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (renderer.enabled) {
			if (updatesElapsed < updatesTillMove) {
				updatesElapsed++;
				return;
			} else {
				updatesElapsed = 0;
			}

			Vector3 move = new Vector3(delta * direction, 0, 0);
			transform.position += move;
		}
	}

	void OnTriggerEnter(Collider collider) {
		Disappear();
	}

	public void Disappear() {
		renderer.enabled = false;
		transform.position = new Vector3(-110, height, 400);
	}
}
