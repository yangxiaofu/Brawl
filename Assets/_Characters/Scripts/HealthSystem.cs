using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	
	public class HealthSystem : MonoBehaviour {
		[SerializeField] float _startingHealth = 100f;
		[SerializeField] float _currentHealth = 100f;
		[SerializeField] float _healthImprovedPerSecond = 5f;
		float _minHealth = 0;
		public float healthAsPercentage{ get{return _currentHealth / _startingHealth;}}
		HealthSystemLogic _healthSystemLogic;

		public void Start()
		{
			_healthSystemLogic = new HealthSystemLogic(_minHealth, _startingHealth);
		}

		void Update()
		{
			var healthToAdd = _healthImprovedPerSecond * Time.deltaTime;
			_healthSystemLogic.IncreaseHealth(_currentHealth, healthToAdd);
		}

		public void TakeDamage(float damageToTake)
		{
			_healthSystemLogic.TakeDamage(_currentHealth, damageToTake);
		}
	}
}

