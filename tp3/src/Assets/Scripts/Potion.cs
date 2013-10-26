using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour
{
	public int heal = 60;
	private Player player;
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) {
			player.Heal (heal);
			Destroy (gameObject);
		}
	}
}
