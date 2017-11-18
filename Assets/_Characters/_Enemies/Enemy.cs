using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Weapons;
using Game.Core;

namespace Game.Characters
{
	public class Enemy : Character
	{
		[Space]
		[Header("Attack Attributes")]
		[SerializeField] float _moveRadius = 10f;
		[SerializeField] float _attackRadius = 1f;
		public float attackRadius{get{return _attackRadius;}}
		public float moveRadius{get{return _moveRadius;}}	
		
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

