using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class EnergySystem : MonoBehaviour {
		[SerializeField] float _startingEnergy = 100f;
		[SerializeField] float _currentEnergy = 100f;
		[SerializeField] float _increasePerSecond = 1f;

		[Space] [Header("Energy Consumption")] 
		[SerializeField] float _energyToConsumeOnJump = 10f;
		public float energyToConsumeOnJump{get{return _energyToConsumeOnJump;}}
		[SerializeField] float _energyToConsumeOnDash = 50f;
		public float energyToConsumeOnDash{get{return _energyToConsumeOnDash;}}
		float _minEnergy = 0;
		public float energyAsPercentage{ get{return _currentEnergy / _startingEnergy;}}
		EnergySystemLogic logic;
		void Start()
		{
			logic = new EnergySystemLogic(_minEnergy, _startingEnergy);
		}

		void Update()
		{
			_currentEnergy = logic.IncreaseEnergy(_currentEnergy, Time.deltaTime * _increasePerSecond);
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
