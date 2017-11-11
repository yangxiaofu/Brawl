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

			DrawDefaultInspector();

			serializedObject.Update();
		}
	}

}
