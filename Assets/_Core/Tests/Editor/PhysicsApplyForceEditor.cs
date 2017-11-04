using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.Tests{
	[CustomEditor(typeof(PhysicsApplyForce))]
	public class PhysicsApplyForceEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.ApplyModifiedProperties();
			var behaviour = serializedObject.targetObject as PhysicsApplyForce;

			DrawDefaultInspector();			

			if (GUILayout.Button("Apply Force"))
			{
				behaviour.ApplyForce();
			}

			EditorGUILayout.Space();

			if (GUILayout.Button("Reset Character"))
			{
				behaviour.Reset();
			}
			serializedObject.Update();
		}
	}
}

