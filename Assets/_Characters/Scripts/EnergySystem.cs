using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class EnergySystem : MonoBehaviour {
		[SerializeField] float _startingEnergy = 100f;
		[SerializeField] float _currentEnergy = 100f;
		[SerializeField] float _increasePerSecond = 1f;
		public float energyAsPercentage{ get{return _currentEnergy / _startingEnergy;}}
		EnergySystemLogic logic;

		void Start()
		{
			logic = new EnergySystemLogic();
		}

		void Update()
		{
			_currentEnergy = logic.IncreaseEnergy(_currentEnergy, Time.deltaTime * _increasePerSecond, _startingEnergy);
		}

		public void ConsumeEnergy(float energyToConsume)
		{
			_currentEnergy = logic.ConsumeEnergy(_currentEnergy, energyToConsume);
		}

		public bool HasEnergy(float energyToConsume)
		{
			return logic.HasEnergy(_currentEnergy, energyToConsume);
		}

	}

}
