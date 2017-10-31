using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
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

