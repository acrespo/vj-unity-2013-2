using UnityEngine;
using System.Collections.Generic;

public class EnemyManager {

	private Dictionary<Vector2, bool> spawnPoints = new Dictionary<Vector2, bool>();
	
	private int tanksLeft = 10;
	
	private int spawned = 0;
	
	private Transform parent;
	
	public delegate void AllKilled();
	
	private AllKilled allKilled;
	
	public EnemyManager(Transform parent, AllKilled allKilled) {
		this.parent = parent;
		this.allKilled = allKilled;
	}
	
	public void Reset() {
		spawnPoints.Clear();
		tanksLeft = 10;
		spawned = 0;
	}
	
	public void AddPoint(Vector2 point) {
		Debug.Log (point);
		spawnPoints.Add(point, false);
	}
	
	public void Spawn() {
		Vector2[] keys = new Vector2[spawnPoints.Count];
		spawnPoints.Keys.CopyTo(keys, 0);
		
		foreach (Vector2 point in keys) {
			if (!spawnPoints[point] && tanksLeft > 0) {
				GameObject enemy = ObjectPool.Instance.GetObject("Enemy");
				
				enemy.transform.position = new Vector3(point.x, 0f, point.y);
				enemy.transform.parent = parent;
				enemy.GetComponent<Destroyable>().onDestroy = EnemyDestroy;
				enemy.GetComponent<Enemy>().SpawnPoint = point;
				
				MeshRenderer[] renderers = enemy.GetComponentsInChildren<MeshRenderer>() as MeshRenderer[];
				
				foreach (MeshRenderer renderer in renderers) {
					Material[] materials = renderer.materials;
					foreach (Material material in materials) {
						material.SetColor("_Color", Color.red);
					}
				}
				
				spawnPoints[point] = true;
				tanksLeft --;
				
				spawned++;
			}
		}
	}
	
	public void EnemyDestroy(GameObject obj) {
		
		Vector2 point = obj.GetComponent<Enemy>().SpawnPoint;
		spawnPoints[point] = false;
		
		spawned--;
		if (tanksLeft == 0 && spawned == 0) {
			allKilled();
		} else {
			Spawn();
		}
	}
	
	public int GetTanksLeft(){
		return tanksLeft;
	}
}
