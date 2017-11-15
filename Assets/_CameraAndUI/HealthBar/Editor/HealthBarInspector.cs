using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof (Healthbar))]
public class HealthBarInspector : Editor {

	[SerializeField] int initialHealth;

	GUIStyle title;
	Camera cam;
	Healthbar healthbar;
	ApplyDamage[] ad;

	bool advanced;

	public Texture2D t;

	void Awake(){
		var hb = target as Healthbar;
		healthbar = hb;
	}

	public override void OnInspectorGUI(){

		//base.OnInspectorGUI();

		ad = FindObjectsOfType<ApplyDamage>();

		if (EditorApplication.isPlaying) {
			GUIStyle gs = new GUIStyle(EditorStyles.label);
			gs.alignment = TextAnchor.MiddleCenter;
			gs.normal.textColor = Color.yellow;
			GUILayout.Label("Exit play mode to modify settings!", gs);
		}

		else {

			title = new GUIStyle(EditorStyles.whiteLargeLabel);
			title.fontStyle = FontStyle.Bold;
			initialHealth = EditorPrefs.GetInt("ih");

			EditorGUILayout.LabelField("Player options", title);
			healthbar.playerTag = EditorGUILayout.TagField("Player's tag", healthbar.playerTag);

			GameObject locator = GameObject.FindGameObjectWithTag(healthbar.playerTag);

			if (locator) {
				EditorGUILayout.LabelField("Player assigned to: " + locator.gameObject.name);
				if (GUILayout.Button("Select Player Gameobject"))
					Selection.activeGameObject = locator;
			}
			else {
				GUIStyle s = new GUIStyle(EditorStyles.whiteLargeLabel);
				s.normal.textColor = Color.red;
				s.alignment = TextAnchor.MiddleCenter;
				s.fontStyle = FontStyle.Bold;
				EditorGUILayout.LabelField("Player not found!", s);
			}

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Healthbar Behavior", title);

			initialHealth = EditorGUILayout.IntSlider("Player's health", initialHealth, 1, 100);
			EditorPrefs.SetInt("ih", initialHealth);

			healthbar.displayYellowBar = EditorGUILayout.IntSlider("Turn yellow at", healthbar.displayYellowBar, 1, 100);
			healthbar.displayRedBar = EditorGUILayout.IntSlider("Display red at", healthbar.displayRedBar, 1, 100);

			healthbar.chosenTheme = (Theme)EditorGUILayout.EnumPopup("Chosen theme", healthbar.chosenTheme);

			healthbar.positioning = (Healthbar.Positioning)EditorGUILayout.EnumPopup("Positioning", healthbar.positioning);

			EditorGUILayout.Space();

			healthbar.showHealthValue = EditorGUILayout.Toggle("Show Text", healthbar.showHealthValue);

			if (healthbar.showHealthValue) {
				EditorGUILayout.LabelField("Font properties", title);
				healthbar.fontSize = EditorGUILayout.IntSlider("Font size", healthbar.fontSize, 5, 25);

				healthbar.chosenFont = (FontNames)EditorGUILayout.EnumPopup("Font type", healthbar.chosenFont);

				healthbar.displayCritical = EditorGUILayout.IntSlider("Display critical at", healthbar.displayCritical, 1, 60);
				healthbar.healthMessage = EditorGUILayout.TextField("Health message", healthbar.healthMessage);
				healthbar.showCritical = EditorGUILayout.Toggle("Show Critical message", healthbar.showCritical);
				if (healthbar.showCritical)
					healthbar.criticalMessage = EditorGUILayout.TextField("Critical message", healthbar.criticalMessage);
			}

			if (GUI.changed) {
				healthbar.health = initialHealth * 10;
				EditorUtility.SetDirty(healthbar);
			}
			EditorGUILayout.Knob(new Vector2(70, 70), healthbar.health / 10, 1, 100, "Player's health", Color.red, Color.green, true);

			GUIStyle info = new GUIStyle(EditorStyles.label);
			info.normal.textColor = Color.green;

			//obj = (GameObject) EditorGUILayout.ObjectField("Find Dependency", obj, typeof(GameObject));

			//obj = (ApplyDamage) EditorGUILayout.ObjectField("Examine class", obj, typeof(ApplyDamage), true);

			advanced = EditorPrefs.GetBool("advanced");
			advanced = EditorGUILayout.Toggle("Advanced options", advanced);
			EditorPrefs.SetBool("advanced", advanced);


			if (advanced) {
				EditorGUILayout.LabelField("ApplyDamage classes present: " + ad.Length, EditorStyles.miniLabel);
				for (int i = 0; i < ad.Length; i++) {
					if (GUILayout.Button("Select " + i + " is " + ad[i].transform.name)) {
						Selection.activeGameObject = ad[i].gameObject;
					}
				}
			}
		}
	}	
}
