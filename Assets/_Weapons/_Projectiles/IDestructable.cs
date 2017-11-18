using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons
{
	///<summary>IDestructable is used to create destruction to objects that have a Destructable script on it.  These are added to objects that cause the destruction</summary>
	public interface IDestructable { 
		float GetDamage();
	}
}

