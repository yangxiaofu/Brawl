using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class EnergySystem : MonoBehaviour {
		[SerializeField] float _startingEnergy = 100f;
		[SerializeField] float _currentEnergy = 100f;
		[SerializeField] float _increasePerSecond = 1f;
		public float energyAsPercentage{ get{return _currentEnergy / _startingEnergy;}}

		void Update()
		{
			_currentEnergy += _increasePerSecond * Time.deltaTime;
			_currentEnergy = Mathf.Clamp(_currentEnergy, 0, _startingEnergy);
		}

		public void ConsumeEnergy(float energyToConsume)
		{
			_currentEnergy -= energyToConsume;
			_currentEnergy = Mathf.Clamp(_currentEnergy, 0, _startingEnergy);
		}

		public bool HasEnergy(float energy)
		{
			return (_currentEnergy > energy);
		}

	}

}
