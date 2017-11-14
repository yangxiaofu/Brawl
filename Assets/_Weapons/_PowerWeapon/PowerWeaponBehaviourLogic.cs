using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons{
	public class PowerWeaponBehaviourLogic{

		public bool CanCharge(bool chargingAllowed, bool hasEnergy)
		{
			return (chargingAllowed && hasEnergy);
		}
	}

}

