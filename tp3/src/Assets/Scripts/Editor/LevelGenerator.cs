﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(Level))]
public class LevelGenerator : Editor {
	
	[SerializeField]
	int desiredRooms;
	
	[SerializeField]
	float trapFactor;
	
	[SerializeField]
	int seedValue;
	
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		desiredRooms = EditorGUILayout.IntField("Target Room Count", desiredRooms);
		trapFactor = EditorGUILayout.FloatField("Path Trap Factor", trapFactor);
		trapFactor = EditorGUILayout.IntField("Seed Value", seedValue);
		
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
			
			Generator.LevelGenerator g;
			if (seedValue != 0) {
		 		g = new Generator.LevelGenerator(level, desiredRooms, trapFactor);
			} else {
				g = new Generator.LevelGenerator(level, desiredRooms, trapFactor, seedValue);
			}
			g.Generate();
			g.Populate(level);
			EditorUtility.SetDirty(go);
			
		}
	}
	
}
