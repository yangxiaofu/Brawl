using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using System;

namespace Game.Items{
	public abstract class WeaponConfig : ItemConfig {
		[Header("Weapon Config Specific")]
		[SerializeField] bool _isWeapon = false;
		public override bool IsWeapon() { return _isWeapon;}
		[Space]
		[SerializeField] GameObject _particleSystemPrefab;
		public GameObject GetParticleSystemPrefab() { return _particleSystemPrefab;}
		[SerializeField] Transform _weaponGripTransform;
		public Transform weaponGripTransform{get{return _weaponGripTransform;}}
		public abstract void Use();
    }
}

