using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	public class WeaponSystemLogic {

		public bool LowOnAmmo(float currentAmmo, float lowAmmoThreshold)
		{
			return currentAmmo < lowAmmoThreshold;
		}
	}
}

