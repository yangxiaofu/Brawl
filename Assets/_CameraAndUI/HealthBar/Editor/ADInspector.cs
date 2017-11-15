using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ApplyDamage))]
public class ADInspector : Editor {

	int health;

	public override void OnInspectorGUI() {

		//base.OnInspectorGUI();

		var ad = target as ApplyDamage;

		if (EditorApplication.isPlaying) {
			GUIStyle gs = new GUIStyle(EditorStyles.label);
			gs.alignment = TextAnchor.MiddleCenter;
			gs.normal.textColor = Color.yellow;
			GUILayout.Label("Exit play mode to modify settings!", gs);
		}
		else {
			ad.IsThisHealingPlayer = EditorGUILayout.Toggle("Is this healing player?", ad.IsThisHealingPlayer);

			if (ad.IsThisHealingPlayer) {
				ad.IsThisPickupable = EditorGUILayout.Toggle("Is this pickupable?", ad.IsThisPickupable);

				if (ad.IsThisPickupable) {
					health = ad.AmountOfHealth/10;
					health = EditorGUILayout.IntSlider("Health points gained", health, 1, 100);
					int i = health;
					float f = (int)i;
					f = f/100;
					Rect r = EditorGUILayout.BeginVertical();
					EditorGUI.ProgressBar(r, f, "Gained health when picked up");
					GUILayout.Space(18);
					EditorGUILayout.EndVertical();
				}
				else {
					ad.AmountOfHealth = EditorGUILayout.IntSlider("Heal up speed", ad.AmountOfHealth, 1, 10);
					int ih = ad.AmountOfHealth;
					float fh = (int)ih;
					fh = fh/10;
					Rect rh = EditorGUILayout.BeginVertical();
					EditorGUI.ProgressBar(rh, fh, "Heal up speed");
					GUILayout.Space(18);
					EditorGUILayout.EndVertical();
				}

			}

			else {
				ad.instantDeath = EditorGUILayout.Toggle("Kill player instantly", ad.instantDeath);

				if (!ad.instantDeath) {
					ad.IsThisPickupable = false;
					ad.AmountOfHealth = EditorGUILayout.IntSlider("Damage speed", ad.AmountOfHealth, 1, 10);
					int i = ad.AmountOfHealth;
					float f = (int)i;
					f = f/10;
					Rect r = EditorGUILayout.BeginVertical();
					EditorGUI.ProgressBar(r, f, "Damage speed");
					GUILayout.Space(18);
					EditorGUILayout.EndVertical();
				}
			}
					
			if (GUI.changed) {
				if (ad.IsThisPickupable)
					ad.AmountOfHealth = health * 10;
				EditorUtility.SetDirty(ad);
			}
		}
	}
}
