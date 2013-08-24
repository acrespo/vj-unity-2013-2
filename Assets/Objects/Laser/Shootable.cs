using UnityEngine;

public abstract class Shootable : MonoBehaviour {
	
	public Team team;
	
	abstract public void Hit();
}

