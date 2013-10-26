using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour
{
	public int heal = 60;
	private GameObject player;
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) {
			player.GetComponent<Player> ().Heal (heal);
			Destroy (gameObject);
		}
	}
}
