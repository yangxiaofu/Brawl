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
		[Tooltip("The enemy will start to move towards the targeted player, if the player is within this radius.")]
		[SerializeField] float _moveRadius = 10f;
		public float moveRadius{get{return _moveRadius;}}	

		[Tooltip("If the enemy is within this radius, they will start attacking the Player.")]
		[SerializeField] float _attackRadius = 1f;
		public float attackRadius{get{return _attackRadius;}}

		[Tooltip("This applies to shooter enemies.  If the player is within this distance, the enemy will move away from the player.")]
		[SerializeField] float _runawayDistance = 2f;
		public float runawayDistance{get{return _runawayDistance;}}
		
		void Awake()
		{
			InitializeCharacterVariables();
			_weaponSystem.InitializeWeaponSystem();
			_anim = GetComponent<Animator>();
		}

		void Start()
		{
			GetComponent<NavMeshAgent>().stoppingDistance = _attackRadius;
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.black;
			Gizmos.DrawWireSphere(this.transform.position, _moveRadius);

			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(this.transform.position, _attackRadius);

			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(this.transform.position, _runawayDistance);
		}

		void OnAnimatorIK(int layerIndex)
		{
			if (!GetComponent<EnemyAI>()) 
				Debug.LogError("You need to keep Enemy AI on " + this.gameObject.name + " in order to use the OnAnimatorIK.");
			
			if (GetComponent<EnemyAI>().target == null)
				return;

			var pointDirection = (GetComponent<EnemyAI>().target.transform.position - this.transform.position).normalized;
			LookAtTarget(pointDirection);
			SetIKPosition(pointDirection);
		}
	}

}

