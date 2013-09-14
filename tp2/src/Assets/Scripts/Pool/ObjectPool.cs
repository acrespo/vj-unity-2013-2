using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool>
{
	private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();
	
	public PoolConfig[] config;
	
	void Start() {
		foreach (PoolConfig poolConfig in config) {
			Pool pool = new Pool(poolConfig);
			pools.Add (pool.name, pool);
		}
	}
	
	public GameObject GetObject(string type) {
		return pools[type].GetObject();
	}
	
	public void Return(GameObject obj) {
		
		if (gameObject.GetComponent<Poolable>()) {	
			pools[obj.GetComponent<Poolable>().poolId].Return(obj);
		} else {
			GameObject.Destroy(gameObject);
		}
	}
	
	[Serializable]
	public class PoolConfig {
		
		public GameObject prefab;
		
		public int poolLimit = -1;
		
		public int startCount = 0;
	}
	
	class Pool {
		
		private Queue<GameObject> objects = new Queue<GameObject>();
		
		private PoolConfig config;
		
		public string name;
		
		public Pool(PoolConfig config) {
			this.config = config;
			
			name = config.prefab.GetComponent<Poolable>().poolId;
			for (int i = 0; i < config.startCount; i++) {
				Return(GameObject.Instantiate(config.prefab) as GameObject);
			}
		}
		
		public GameObject GetObject() {
			if (objects.Count == 0) {
				return GameObject.Instantiate(config.prefab) as GameObject;
			} else {
				GameObject obj = objects.Dequeue();
				obj.SetActive(true);
				
				return obj;
			}
		}
		
		public void Return(GameObject obj) {
			if (config.poolLimit == -1 || config.poolLimit >= objects.Count) {
				objects.Enqueue(obj);
				obj.SetActive(false);
			} else {
				GameObject.Destroy(obj);
			}
		}
		
	}
}
