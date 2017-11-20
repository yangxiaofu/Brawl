using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Weapons;

namespace Game.Core
{
	public struct HittableArgs{
		public RaycastHit[] hits;
		public Vector3 explosionPosition;
		public Character shootingCharacter;
		public WeaponConfig weaponConfig;
		public HittableArgs(RaycastHit[] hits, Vector3 explosionPosition, Character shootingCharacter, WeaponConfig weaponConfig){
			this.hits = hits;
			this.explosionPosition = explosionPosition;
			this.shootingCharacter = shootingCharacter;
			this.weaponConfig = weaponConfig;
		}
	}
}

