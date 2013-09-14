using UnityEngine;
using System.Collections;

public class Poolable : MonoBehaviour {

	public string poolId;
	
	public void Return() {
		ObjectPool.Instance.Return(gameObject);
	}
}
