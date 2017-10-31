using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.Characters{
	[CustomEditor(typeof(Enemy))]
	public class EnemyEditor : Editor {

		public override void OnInspectorGUI()
		{
			serializedObject.ApplyModifiedProperties();

			DrawDefaultInspector();

			var enemy = serializedObject.targetObject as Enemy;

			if (GUILayout.Button("Jump"))
			{
				
				enemy.Jump();
			}

			if (GUILayout.Button("Dash"))
			{
				enemy.Dash();	
			}

			serializedObject.Update();
		}
	}
}

