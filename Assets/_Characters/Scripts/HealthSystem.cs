using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	
	public class HealthSystem : MonoBehaviour {
		[SerializeField] float _startingHealth = 100f;
		[SerializeField] float _currentHealth = 100f;
		[SerializeField] float _healthImprovedPerSecond = 5f;
		public float healthAsPercentage{ get{return _currentHealth / _startingHealth;}}

		void Update()
		{
			var healthToAdd = _healthImprovedPerSecond * Time.deltaTime;
			AddHealth(healthToAdd);
		}

		public void AddHealth(float healthToAdd)
		{
			_currentHealth += healthToAdd;
			_currentHealth = Mathf.Clamp(_currentHealth, 0, _startingHealth);
		}

		public void TakeDamage(float damageToTake)
		{
			_currentHealth -= damageToTake;
			_currentHealth = Mathf.Clamp(_currentHealth, 0, _startingHealth);
		}
	}
}

