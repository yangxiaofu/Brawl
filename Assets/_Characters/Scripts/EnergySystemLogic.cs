using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class EnergySystemLogic {
		float _minimumEnergy = 0;
		public bool HasEnergy(float currentEnergyLevel, float energyToConsume)
		{
			return (currentEnergyLevel >= energyToConsume ? true : false);
		}

		public float ConsumeEnergy(float currentEnergy, float energyToConsume)
		{
			currentEnergy -= energyToConsume;
			currentEnergy = Mathf.Clamp(currentEnergy, _minimumEnergy, currentEnergy);
			return currentEnergy;
		}

		public float IncreaseEnergy(float currentEnergy, float energyToAdd, float maxEnergy)
		{
			currentEnergy += energyToAdd;
			currentEnergy = Mathf.Clamp(currentEnergy, _minimumEnergy, maxEnergy);
			return currentEnergy;
		}

	}
}

