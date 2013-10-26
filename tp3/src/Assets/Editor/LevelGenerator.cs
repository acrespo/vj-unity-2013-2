using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(Level))]
public class LevelGenerator : Editor {
	
	[SerializeField]
	private int desiredRooms;
	
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		desiredRooms = EditorGUILayout.IntField("Target Room Count", desiredRooms);
		
		if (GUILayout.Button("Generate")) {
			
			Level level = target as Level;
			GameObject go = level.gameObject;
			List<GameObject> gos = new List<GameObject>();
			foreach (Transform child in go.transform) {
				gos.Add(child.gameObject);
			}
			foreach (GameObject toDie in gos) {
				GameObject.DestroyImmediate(toDie);
			}
			
			Console.WriteLine ("running");

		 	Generator.LevelGenerator g = new Generator.LevelGenerator(level, desiredRooms);
			g.Generate();
			g.Populate(level);
			EditorUtility.SetDirty(go);
			
		}
	}
	
}
