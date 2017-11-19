using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Weapons{
	public struct ProjectileBehaviourArgs{
		public Character shootingCharacter;
		public Vector3 travelDirection;
		public WeaponConfig rangeWeaponConfig;
		public ProjectileBehaviourArgs(Character shootingCharacter, Vector3 travelDirection, WeaponConfig rangeWeaponConfig)
		{
			this.shootingCharacter = shootingCharacter;
			this.travelDirection = travelDirection;
			this.rangeWeaponConfig = rangeWeaponConfig;
		}
	}
}

