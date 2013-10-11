using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

[CustomEditor(typeof(Level))]
public class LevelGenerator : Editor {
	
	private int width;
	
	private int height;
	
	private int desiredRooms;
	
	public override void OnInspectorGUI ()
	{
		width = EditorGUILayout.IntField("Width", width);
		height = EditorGUILayout.IntField("Height", height);
		desiredRooms = EditorGUILayout.IntField("Target Room Count", desiredRooms);
		
		if (GUILayout.Button("Generate")) {
			
			Level level = target as Level;
			GameObject go = level.gameObject;
			foreach (Transform child in go.transform) {
				GameObject.DestroyObject(child.gameObject);
			}
			
			Console.WriteLine ("running");

			new Generator.LevelGenerator(width, height, desiredRooms).Generate();
		}
	}
	
}
