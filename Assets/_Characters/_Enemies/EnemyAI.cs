using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

namespace Game.Characters{
	public class EnemyAI : MonoBehaviour {
		Enemy _enemy;
		NavMeshAgent _agent;
		Animator _anim;
		const string IS_WALKING = "IsWalking";
		const string ATTACK = "Attack";
		MyPlayer _player;

		void Start()
		{
			_agent = GetComponent<NavMeshAgent>();
			_anim = GetComponent<Animator>();
			_enemy = GetComponent<Enemy>();
		}

		[Task] 
		bool PlayerIsWithinMoveRadius()
		{
			var distance = Vector3.Distance(this.transform.position, _player.transform.position);
			return distance < _enemy.moveRadius;
		}

		[Task]
		void FindPlayer()
		{
			_player = GameObject.FindObjectOfType<MyPlayer>();
			Task.current.Succeed();
		}

		[Task]
		bool SetWalkAnimation()
		{
			_anim.SetBool(IS_WALKING, true);
			return true;
		}

		[Task]
		bool SetIdleAnimation()
		{
			_anim.SetBool(IS_WALKING, false);
			return true;
		}

		[Task]
		bool StayPut()
		{
			_agent.SetDestination(this.transform.position);
			return true;
		}
	
		[Task]
		void MoveToPlayer()
		{
			_agent.SetDestination(_player.transform.position);
			Task.current.Succeed();
		}

		[Task]
		bool PlayerIsWithinStoppingDistance()
		{
			var distance = Vector3.Distance(this.transform.position, _player.transform.position);
			return distance < _agent.stoppingDistance;
		}

		[Task]
		bool PlayerIsInAttackRadius()
		{
			if (_player == null)
				return false;

			var distanceFromPlayer = Vector3.Distance(this.transform.position, _player.transform.position);
			return distanceFromPlayer < _enemy.attackRadius;
		}

		[Task]
		void PlayHitAnimation()
		{
			_agent.SetDestination(this.transform.position);
			_anim.SetTrigger(ATTACK);
			Task.current.Succeed();
		}
		
	}
}

