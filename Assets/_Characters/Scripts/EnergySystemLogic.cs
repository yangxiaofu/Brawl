using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class EnergySystemLogic {
		public bool HasEnergy(float currentEnergyLevel, float energyToConsume)
		{
			return (currentEnergyLevel >= energyToConsume ? true : false);
		}

	}
}

