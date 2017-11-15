using UnityEngine;
using System.Collections;

//These two classes are self-explanatory. Variables are serialized and a List<> is created by SpriteDatabase class based on
//what is declared here. (These classes are in project, but not anywhere in the scene)

[System.Serializable]
public class Sprites {
	public Sprite HealthBar_Frame;
	public Sprite GreenBar;
	public Sprite RedBar;
	public Sprite YellowBar;
	public Theme theme;
}

public enum Theme {
		Bar1,
		Bar2,
		Bar3,
		Medieval,
		Horror,
		Medieval2,
		Scifi,
		Heart,
		Cyber,
		Fantasy
}

[System.Serializable]
public class Fonts {
	public Font fontFile;
	public FontNames fontNames;
}

public enum FontNames {
	factDefault,
	Platformer,
	Horror,
	Modern,
	Plastic,
	Retro
}
