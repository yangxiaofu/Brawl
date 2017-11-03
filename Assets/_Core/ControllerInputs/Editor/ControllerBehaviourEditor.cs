using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game.Core.ControllerInputs;

namespace Game.UI{
	[CustomEditor(typeof(PS4ControllerBehaviour))]
	public class ControllerBehaviourEditor : Editor 
	{


		public override void OnInspectorGUI()
		{
			serializedObject.ApplyModifiedProperties();
			var behaviour = serializedObject.targetObject as PS4ControllerBehaviour;

			DrawDefaultInspector();

			if (GUILayout.Button("Deactivate Character"))
			{
				behaviour.DeactivateCharacter();
			}

			EditorGUILayout.Space();

			if (GUILayout.Button("Activate Character"))
			{
				behaviour.ActivateCharacter();
			}

			serializedObject.Update();
		}
	}

}
