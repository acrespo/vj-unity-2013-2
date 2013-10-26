using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{
	public int damage = 2;
	private GameObject player;
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) {
			player.GetComponent<Player> ().Damage (damage);
		}
	}
}
