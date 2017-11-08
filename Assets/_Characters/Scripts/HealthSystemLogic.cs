using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class HealthSystemLogic {
		private readonly float _minHealth;
		private readonly float _maxHealth;

		public HealthSystemLogic(float minHealth, float maxHealth)
		{
			_maxHealth = maxHealth;
			_minHealth = minHealth;
		}
		public float IncreaseHealth(float currentHealth, float healthToIncrease)
		{
			currentHealth += healthToIncrease;
			currentHealth = Mathf.Clamp(currentHealth, _minHealth, _maxHealth);
			return currentHealth;
		}

		public float TakeDamage(float currentHealth, float damageToTake){
			currentHealth -= damageToTake;
			currentHealth = Mathf.Clamp(currentHealth, _minHealth, _maxHealth);
			return currentHealth;
		}

	}

}
