using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Weapons;

namespace Game.Core
{
	public class Hittable
	{
		protected RaycastHit[] _hits;
		protected Vector3 _explosionPosition;
		protected Character _shootingCharacter;
		protected WeaponConfig _weaponConfig;
	}
}

