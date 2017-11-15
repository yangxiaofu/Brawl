using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpriteDatabase))]
public class SDinspector : Editor {

	public override void OnInspectorGUI() {

		//base.OnInspectorGUI();

		GUIStyle gs = new GUIStyle(EditorStyles.label);
		gs.alignment = TextAnchor.MiddleCenter;
		gs.normal.textColor = Color.yellow;

		GUILayout.Label("Database that contains all 10 health bars", gs);

	}

}
