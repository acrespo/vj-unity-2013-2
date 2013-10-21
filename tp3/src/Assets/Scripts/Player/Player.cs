using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public enum PlayerState
	{
		Normal = 0,
		Dying = 1
	}
	
	public float maxLife = 100;
	private PlayerState state;
	private float life;
	
	void Awake ()
	{
		life = maxLife;
		state = PlayerState.Normal;
	}
	
	public PlayerState GetState ()
	{
		return state;
	}
	
	public void Damage (float amount)
	{
		life -= amount;
		if (life <= 0) {
			state = PlayerState.Dying;
		}
	}
}
