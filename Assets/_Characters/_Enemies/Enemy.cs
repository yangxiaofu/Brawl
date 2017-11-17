using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Weapons;

namespace Game.Characters
{
	public class Enemy : Character
	{
		[SerializeField] float _moveRadius = 10f;
		[SerializeField] float _attackRadius = 1f;
		public float attackRadius{get{return _attackRadius;}}
		public float moveRadius{get{return _moveRadius;}}

		[Header("Health System")] //TODO: Refactor out later.
		float _currentHealth = 100f;
		float _startingHealth = 100f;
		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent(typeof(IDestructable)))
				Destroy(this.gameObject);
		}

		void Awake()
		{
			InitializeCharacterVariables();
			_weaponSystem.InitializeWeaponSystem();
		}


		void Start()
		{
			GetComponent<NavMeshAgent>().stoppingDistance = _attackRadius;
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.black;
			Gizmos.DrawWireSphere(this.transform.position, _moveRadius);
		}
	}

}

