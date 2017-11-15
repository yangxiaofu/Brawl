using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Healthbar))]
public class SpriteDatabase : MonoBehaviour {

	//This class holds all information (sprites and gui skin) that Healthbar class needs.
	//Drag Sprites to their respective fields and mark their category.
	//Note: One category (eg. Horror) can only be used once, so if you want to add another Horror theme, 
	//you have to open Sprites class and modify Theme enum, logically. (This class works with a List<>
	//created using serialized variables declared in Sprites class.)


	public List<Sprites> sprites = new List<Sprites>();

	public List<Fonts> fonts = new List<Fonts>();

}
