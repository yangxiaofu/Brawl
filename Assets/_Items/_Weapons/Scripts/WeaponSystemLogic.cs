using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	public class WeaponSystemLogic {

		public bool LowOnAmmo(float currentAmmo, float lowAmmoThreshold)
		{
			return currentAmmo < lowAmmoThreshold;
		}

		public float IncreaseAmmo(float currentAmmo, float increaseAmount, float maxAmount)
		{
			currentAmmo += increaseAmount;
			currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmount);
			return currentAmmo;
		}
	}
}

