using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Weapons;

namespace Game.Weapons{
	public class WeaponSystem : MonoBehaviour {
		[SerializeField] WeaponConfig _primaryWeapon;
		public WeaponConfig GetPrimaryWeapon()
		{
			return _primaryWeapon;
		}
		public void UpdateWeapon(WeaponConfig weaponConfig)
		{
			_primaryWeapon = weaponConfig;
		}
	}
}

