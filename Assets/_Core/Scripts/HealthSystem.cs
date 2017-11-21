using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Characters;

namespace Game.Core{
	public class HealthSystem : MonoBehaviour, IDamageable //TODO: Separate Character Health System from Enemy Health System through Inheritance later. 
	{
		[Tooltip("Each time the player is hit, the player will scale depending on the factor provided here.")]
		[SerializeField] float _currentHealth = 100f;
		[SerializeField] float _startingHealth = 100f;
		[Space]
		[SerializeField] GameObject _killParticleEffectPrefab;
		public GameObject killParticleEffectPrefab{get{return _killParticleEffectPrefab;}}
		
		HealthSystemLogic _healthSystemLogic;
		public float healthAsPercentage{get{return _currentHealth/_startingHealth;}}
		void Start()
		{
			_currentHealth = _startingHealth;
			_healthSystemLogic = new HealthSystemLogic();
		}

		public void Heal(float healToAdd)
		{
			_currentHealth += healToAdd;
			_currentHealth = Mathf.Clamp(_currentHealth, 0, _startingHealth);
		}

		public void DealDamage(float damage)
		{
			_currentHealth -= damage;

			if (_currentHealth <= 0)
			{
				if (GetComponent<Character>() && !GetComponent<Character>().isDead)
				{
					if (GetComponent<NavMeshAgent>())
					{
						GetComponent<NavMeshAgent>().enabled = false;
						StartCoroutine(GetComponent<Character>().KillCharacter());
					}
					
					return;
				}
				
				if (GetComponent<EnemyNest>())
					Destroy(this.gameObject);
			}
		}
	}
}

