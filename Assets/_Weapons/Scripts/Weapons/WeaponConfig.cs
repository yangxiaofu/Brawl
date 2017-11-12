using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;
using System;

namespace Game.Weapons{
	public abstract class WeaponConfig : ItemConfig 
	{
		[Header("Weapon Config Specific")]
		[SerializeField] bool _isWeapon = false;
		public override bool IsWeapon() { return _isWeapon;}
		
		[Space]
		[SerializeField] Transform _weaponGripTransform;
		public Transform weaponGripTransform{get{return _weaponGripTransform;}}
		public abstract BlastConfig GetBlastConfig();
		public abstract float GetBlastDelayAfterCollision();
		
    }
}

