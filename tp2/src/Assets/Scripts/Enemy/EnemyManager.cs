using UnityEngine;
using System.Collections.Generic;

public class EnemyManager {

	private Dictionary<Vector2, bool> spawnPoints = new Dictionary<Vector2, bool>();
	
	private int tanksLeft = 10;
	
	private Transform parent;
	
	public EnemyManager(Transform parent) {
		this.parent = parent;
	}
	
	public void Reset() {
		spawnPoints.Clear();
		tanksLeft = 10;
	}
	
	public void AddPoint(Vector2 point) {
		spawnPoints.Add(point, false);
	}
	
	public void Spawn() {
		foreach (Vector2 point in spawnPoints.Keys) {
			if (!spawnPoints[point] && tanksLeft > 0) {
				GameObject enemy = ObjectPool.Instance.GetObject("Enemy");
				
				enemy.transform.position = new Vector3(point.x, 0.6f, point.y);
				enemy.transform.parent = parent;
				enemy.GetComponent<Destroyable>().onDestroy = EnemyDestroy;
				enemy.GetComponent<Enemy>().SpawnPoint = point;
				
				tanksLeft --;
			}
		}
	}
	
	public void EnemyDestroy(GameObject obj) {
		Vector2 point = obj.GetComponent<Enemy>().SpawnPoint;
		spawnPoints[point] = false;
		
		Spawn();
	}
}
