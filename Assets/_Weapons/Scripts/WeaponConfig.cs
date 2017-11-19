using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;
using System;

namespace Game.Weapons{
	public abstract class WeaponConfig : ItemConfig 
	{	
		[SerializeField] float _damageToDeal;
		public float damageToDeal{get{return _damageToDeal;}}
		[SerializeField] GameObject _impactEffect;
		public GameObject impactEffect{get{return _impactEffect;}}
    }
}

