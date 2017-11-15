using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
[RequireComponent(typeof(SpriteDatabase))]
public class Healthbar : MonoBehaviour {

	//GABROMEDIA@GMAIL.COM

	//Health works on a 1000 (integer) scale so it gives a wider range when it comes to decreasing/increasing the value incrementally
	//Health percentage is stored in healthNormalized integer.

	public int fontSize;
		
	public static int playersHealth;
	
	public int health;
	int healthNormalized;
	GameObject player;
	
	Image frame;
	Image bar;
	
	public int displayCritical;
	public int displayRedBar;
	public int displayYellowBar;
	public string healthMessage;
	public string criticalMessage = "Critical";
	public string playerTag;
	
	Text Message;
	Text Critical;
	
	public bool showHealthValue;
	public bool showCritical;
	
	SpriteDatabase sd;
	
	public Theme chosenTheme;
	public FontNames chosenFont;

	int myTheme;
	int myFontTheme;
	
	public enum Positioning {
		Left,
		Right
	}

	[HideInInspector]
	public bool alive = true;

	//For demo purposes, store player's initial transform (so later it can be respawned there)
	Vector3 startPos;

	//used to choose between left or right alignment
	public Positioning positioning;

	//On Start, assign SpriteDatabse class to 'sd'. (Note: That class can never be missing due to the dependency system)
	//It then runs Debugger() (find it below.) It checks whether the required sprites are assigned in the inspector, etc.
	//Then, it builds hierarchy for GUI (find below)
	void Start(){
		sd = GetComponent<SpriteDatabase>();
		fontSize = Mathf.Clamp(fontSize, 5, 30);
		Debugger();
		BuildHierarchy();
		startPos = player.transform.position;
	}


	//Converts health integer to float value and updates it every frame.
	//Keeps the GUI bar (image) fill amount value synchronized with the health value.
	//Note: healthNormalized cuts the number so that it's on a 100 scale like in every game (it's basically the percentage)
	void FixedUpdate(){

		if (player) {
			if (alive) {
				if (healthNormalized <= 0) {
					alive = false;
					die ();	
				}
				healthNormalized = health / 10;
				//Converts health value to a float (range 0-1) so it can be used for image.fillamount
				float healthValue = health * 0.001f;
				healthValue = Mathf.Clamp (healthValue, 0, 1);

				//Checks if it's time to turn the bar color to red or yellow (replace the sprite basically)
				CheckForBarColor ();

				bar.fillAmount = healthValue;
			}

			DisplayText ();

		} else {
			player = GameObject.FindGameObjectWithTag ("Player");
			if (player == null) {
				Debug.LogError ("Player tag not found in scene!");
				Destroy (this.gameObject);
			}
		}
	}

	void DisplayText(){
		if (showHealthValue)
			Message.text = healthMessage + ": " + healthNormalized.ToString();
		if (healthNormalized <= displayCritical && alive && showCritical) {
			Critical.enabled = true;
		}
		else
			Critical.enabled = false;
	}
	
	//Called by every object affecting player's health.
	//Class that calls it: ApplyDamage
	//See that for more info on how to use it!
	public void ModifyHealth(int amount) {
		if (alive)
			health = health - amount;

		health = Mathf.Clamp(health, 0, 1000);
	}

	//Modify this to change the way of dieing (this just for the demo scene (respawn player at starting location after 2 seconds)
	//Find IENumerator at the very bottom of the code.
	void die(){
		StartCoroutine(Resurrection());
	}

	//Changes bar color depending on what values are set in the inspector
	void CheckForBarColor(){
		if (healthNormalized > displayYellowBar)
			bar.sprite = sd.sprites[myTheme].GreenBar;
		else if (healthNormalized > displayRedBar)
			bar.sprite = sd.sprites[myTheme].YellowBar;
		else
			bar.sprite = sd.sprites[myTheme].RedBar;
	}	

	//Below this line the script is basically loading in the chosen theme based on the selection from the inspector
	//Selection is based on 'Theme' enum
	//-------------------------------------------------------------------------------------------------------------


	 void BuildHierarchy(){

		//Choose anchor position based on positioning enum selection
		Vector2 anchors;

		if (positioning.Equals(Positioning.Left)) {
			anchors = new Vector2(0, 1);
		}
		else {
			anchors = new Vector2(1, 1);
		}
		//Create a canvas
		//---------------------------------------------------------------------------------------
		GameObject canvasObject = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler));
		Canvas c = canvasObject.GetComponent<Canvas>();
		c.pixelPerfect = true;
		c.renderMode = RenderMode.ScreenSpaceOverlay;
		//---------------------------------------------------------------------------------------
		//Create textpanel
		//---------------------------------------------------------------------------------------
		GameObject textPanelObject = new GameObject("textPanel", typeof(CanvasRenderer), typeof(Image));
		RectTransform textPanelRect = textPanelObject.GetComponent<RectTransform>();
		SetAnchors(textPanelRect, anchors);

		textPanelObject.transform.SetParent(canvasObject.transform);

		Vector2 size = new Vector2(270, 40);
		textPanelRect.sizeDelta = size;

		HideImage(textPanelObject);

		//---------------------------------------------------------------------------------------
		//Create imgpanel
		//---------------------------------------------------------------------------------------
		GameObject barPanelObject = new GameObject("barPanel", typeof(CanvasRenderer), typeof(Image));
		RectTransform barPanelRect = barPanelObject.GetComponent<RectTransform>();
		SetAnchors(barPanelRect, anchors);
		barPanelObject.transform.SetParent(canvasObject.transform);
		
		Vector2 barPanelSize = new Vector2(270, 70);
		barPanelRect.sizeDelta = barPanelSize;

		HideImage(barPanelObject);

		//---------------------------------------------------------------------------------------
		//Create critical panel
		//---------------------------------------------------------------------------------------
		GameObject criticalPanelObject = new GameObject("criticalPanel", typeof(CanvasRenderer), typeof(Image));
		RectTransform criticalPanelRect = criticalPanelObject.GetComponent<RectTransform>();
		SetAnchors (criticalPanelRect, anchors);
		criticalPanelObject.transform.SetParent(canvasObject.transform);
		
		Vector2 sizeee = new Vector2(270, 40);
		criticalPanelRect.sizeDelta = sizeee;

		if (positioning.Equals (Positioning.Right)) {
			PositionRight(textPanelObject);
			PositionRight(barPanelObject);
			PositionRight(criticalPanelObject);
		}
		else {
			PositionLeft(textPanelObject);
			PositionLeft(barPanelObject);
			PositionLeft(criticalPanelObject);
		}

		HideImage(criticalPanelObject);

		//--------------------------------------------------------------------------------------
		//Create healthbar (filler content) itself
		//---------------------------------------------------------------------------------------
		GameObject barObject = new GameObject("Healthbar", typeof(CanvasRenderer), typeof(Image));
		RectTransform barRect = barObject.GetComponent<RectTransform>();
		//Position anchors and parent it to canvas
		barObject.transform.SetParent(barPanelObject.transform);
		Centralize(barRect);

		barObject.transform.localPosition = Vector3.zero;

		//Set image type to filled
		bar = barObject.GetComponent<Image>();
		bar.fillMethod = Image.FillMethod.Horizontal;
		bar.type = Image.Type.Filled;
		//----------------------------------------------------------------------------------------
		//Create healthbar frame
		//----------------------------------------------------------------------------------------
		GameObject barFrameObject = new GameObject("Healthbar_frame", typeof(CanvasRenderer), typeof(Image));
		RectTransform barFrameRect = barFrameObject.GetComponent<RectTransform>();
		frame = barFrameObject.GetComponent<Image>();
		//Anchors and parenting to canvas
		barFrameRect.transform.SetParent(barPanelObject.transform);
		Centralize(barFrameRect);
		barFrameObject.transform.localPosition = Vector3.zero;

		//---------------------------------------------------------------------------------------
		//Create Health message text
		//----------------------------------------------------------------------------------------
		GameObject healthMessageObject = new GameObject("Healthbar_Message", typeof(Text));
		RectTransform messageRect = healthMessageObject.GetComponent<RectTransform>();
		healthMessageObject.transform.SetParent(textPanelObject.transform);
		healthMessageObject.transform.localPosition = Vector3.zero;

		Stretch (messageRect);

		Text text = healthMessageObject.GetComponent<Text>();

		text.font = sd.fonts[myFontTheme].fontFile;

		text.fontSize = fontSize;
		text.alignment = TextAnchor.MiddleCenter;

		Message = text;
		//---------------------------------------------------------------------------------------
		//Create Critical message text
		//----------------------------------------------------------------------------------------

		GameObject criticalMessageObject = new GameObject("Healthbar_Critical", typeof(Text));
		RectTransform criticalRect = criticalMessageObject.GetComponent<RectTransform>();
		criticalMessageObject.transform.SetParent(criticalPanelObject.transform);

		Stretch (criticalRect);

		criticalMessageObject.transform.localPosition = Vector3.zero;

		Text criticalText = criticalMessageObject.GetComponent<Text>();
		criticalText.font = sd.fonts[myFontTheme].fontFile;


		criticalText.fontSize = fontSize;
		criticalText.alignment = TextAnchor.MiddleCenter;
		
		Critical = criticalText;
		Critical.text = criticalMessage;
		Critical.enabled = true;

		//----------------------------------------------------------------------------------------
		//Check sprite dimensions and resize them
		//----------------------------------------------------------------------------------------

		//Assign proper frame sprite
		frame.sprite = sd.sprites[myTheme].HealthBar_Frame;

		//Scale it
		Vector2 frameDimensions = new Vector2(frame.sprite.bounds.size.x, frame.sprite.bounds.size.y);
		Vector3 frameScale = new Vector3(frameDimensions.x, frameDimensions.y, 0.1F);
		barFrameRect.transform.localScale = frameScale;

		//Assign proper bar sprite
		bar.sprite = sd.sprites[myTheme].GreenBar;

		//Scale it
		Vector2 barDimensions = new Vector2(bar.sprite.bounds.size.x, bar.sprite.bounds.size.y);
		Vector3 barScale = new Vector3(barDimensions.x, barDimensions.y, 0.1F);
		barRect.transform.localScale = barScale;


		//Set healthNormalized
		healthNormalized = health/10;
	}

	//Position the main panels - modify these only if super necessary
	void PositionLeft(GameObject mainPanel) {
		RectTransform r = mainPanel.GetComponent<RectTransform>();
		if (mainPanel.name == "barPanel") {
			float y = Screen.height - r.sizeDelta.y;
			float x = Screen.width - (Screen.width - r.sizeDelta.x/1.5f);
			mainPanel.transform.position = new Vector3(x, y, 0);
		}
		else if (mainPanel.name == "textPanel") {
			float y = Screen.height - r.sizeDelta.y/2;
			float x = Screen.width - (Screen.width - r.sizeDelta.x/1.5f);
			mainPanel.transform.position = new Vector3(x, y, 0);
		}
		else if (mainPanel.name == "criticalPanel") {
			float y = Screen.height - r.sizeDelta.y*3;
			float x = Screen.width - (Screen.width - r.sizeDelta.x/1.5f);
			mainPanel.transform.position = new Vector3(x, y, 0);
		}
	}

	void PositionRight(GameObject mainPanel) {
		RectTransform r = mainPanel.GetComponent<RectTransform>();
		if (mainPanel.name == "barPanel") {
			float y = Screen.height - r.sizeDelta.y;
			float x = Screen.width - r.sizeDelta.x/1.5f;
			mainPanel.transform.position = new Vector3(x, y, 0);
		}
		else if (mainPanel.name == "textPanel") {
			float y = Screen.height - r.sizeDelta.y/2;
			float x = Screen.width - r.sizeDelta.x/1.5f;
			mainPanel.transform.position = new Vector3(x, y, 0);
		}
		else if (mainPanel.name == "criticalPanel") {
			float y = Screen.height - r.sizeDelta.y*3;
			float x = Screen.width - r.sizeDelta.x/1.5f;
			mainPanel.transform.position = new Vector3(x, y, 0);
		}
	}


	//Returns an integer, the List index of where the theme can be found in SpriteDatabase.
	//If there's no match (eg. SpriteDatabase List isn't assigned in the inspector, it will return the first index (0)
	int FindThemeIndex(Theme t){
		for (int i = 0; i < sd.sprites.Count; i++) {
			if (sd.sprites[i].theme == chosenTheme)
				return i;
		}
		return 0;
	}

	//Returns the font list index
	int FindFontTheme(FontNames f){
		for (int i = 0; i < sd.fonts.Count; i++) {
			if (sd.fonts[i].fontNames == chosenFont)
				return i;
		}
		return 0;
	}
	//Side anchor
	void SetAnchors(RectTransform rectTransform, Vector2 vector2) {
		rectTransform.anchorMax = vector2;
		rectTransform.anchorMin = vector2;
	}
	//Center anchor - used for Health text and Critical text
	void Centralize(RectTransform rectTransform) {
		Vector2 v2 = new Vector2(0.5F, 0.5F);
		rectTransform.anchorMax = v2;
		rectTransform.anchorMin = v2;
	}
	
	void Stretch(RectTransform rectTransform) {
		Vector2 min = new Vector2(0,0);
		Vector2 max = new Vector2(1,1);
		rectTransform.anchorMax = max;
		rectTransform.anchorMin = min;
	}
	//Disable panel's default image displaying
	void HideImage(GameObject g) {
		Image i = g.GetComponent<Image>();
		i.enabled = false;
	}

	//This is only for the demo - to bring player back to life after 2 seconds and respawn it back to the start position
	IEnumerator Resurrection(){
		yield return new WaitForSeconds(2.0F);
		player.transform.position = startPos;
		alive = true;
		health = 1000;

	}

	void Debugger(){
		myTheme = FindThemeIndex(chosenTheme);
		myFontTheme = FindFontTheme(chosenFont);
		player = GameObject.FindGameObjectWithTag(playerTag);
		
		Sprite frame = sd.sprites[myTheme].HealthBar_Frame;
		Sprite green = sd.sprites[myTheme].GreenBar;
		Sprite red = sd.sprites[myTheme].RedBar;
		
		if (player == null) {
			Debug.LogError("No 'Player' tag in scene for Healthbar class!");
			Debug.Break();
		}
		if (sd == null) {
			Debug.LogError("SpriteDatabase class missing!");
			return;
		}
		
		if (frame == null || green == null || red == null) {
			Debug.LogError("Some or all sprites are not assigned for chosen theme in the inspector!");
			Debug.Break();
		}
	}
}
