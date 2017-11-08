using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class EnergySystemLogic {
		private readonly float _minimumEnergy = 0;
		private readonly float _maximumEnergy = 100f;

		public  EnergySystemLogic(float minimumEnergy, float maximumEnergy)
		{
			_minimumEnergy = minimumEnergy;
			_maximumEnergy = maximumEnergy;
		}

		public bool HasEnergy(float currentEnergyLevel, float energyToConsume)
		{
			return (currentEnergyLevel >= energyToConsume ? true : false);
		}

		public float ConsumeEnergy(float currentEnergy, float energyToConsume)
		{
			currentEnergy -= energyToConsume;
			currentEnergy = Mathf.Clamp(currentEnergy, _minimumEnergy, _maximumEnergy);
			return currentEnergy;
		}

		public float IncreaseEnergy(float currentEnergy, float energyToAdd)
		{
			currentEnergy += energyToAdd;
			currentEnergy = Mathf.Clamp(currentEnergy, _minimumEnergy, _maximumEnergy);
			return currentEnergy;
		}

	}
}

