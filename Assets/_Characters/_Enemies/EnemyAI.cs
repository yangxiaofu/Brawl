using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Weapons;
using Panda;

namespace Game.Characters{
	public class EnemyAI : MonoBehaviour {
		Enemy _enemy;
		NavMeshAgent _agent;
		Animator _anim;
		const string IS_WALKING = "IsWalking";
		const string ATTACK = "Attack";
		List<MyPlayer> _players = new List<MyPlayer>();
		MyPlayer _target;
		public MyPlayer target{get{return _target;}}
		void Start()
		{
			_agent = GetComponent<NavMeshAgent>();
			_anim = GetComponent<Animator>();
			_enemy = GetComponent<Enemy>();
			_players = GameObject.FindObjectsOfType<MyPlayer>().ToList(); //TODO: Do later for the nearest player.
		}

		[Task]
		bool EnemyIsDead()
		{
			return GetComponent<Character>().isDead;
		}

		[Task] 
		bool PlayerIsWithinMoveRadius()
		{
			var distance = Vector3.Distance(this.transform.position, _target.transform.position);
			return distance < _enemy.moveRadius;
		}

		[Task]
		void FindPlayer()
		{
			_target = _players[0];

			for(int i = 0; i < _players.Count; i++)
			{
				var distanceFromTarget = Vector3.Distance(_target.transform.position, this.transform.position);
				var distanceFromPlayer = Vector3.Distance(_players[i].transform.position, this.transform.position);

				if (distanceFromPlayer < distanceFromTarget)
					_target = _players[i];
			}

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
			if (_agent.enabled)
			{
				_agent.SetDestination(_target.transform.position);
				Task.current.Succeed();
			} 
			else 
			{
				Task.current.Fail();
			}
		}

		[Task]
		bool PlayerIsWithinStoppingDistance()
		{
			var distance = Vector3.Distance(this.transform.position, _target.transform.position);
			return distance < _agent.stoppingDistance;
		}

		[Task]
		bool PlayerIsInAttackRadius()
		{
			if (_target == null)
				return false;

			var distanceFromPlayer = Vector3.Distance(this.transform.position, _target.transform.position);
			return distanceFromPlayer < _enemy.attackRadius;
		}

		[Task]
		void PlayHitAnimation()
		{
			_agent.SetDestination(this.transform.position);
			_anim.SetTrigger(ATTACK);
			Task.current.Succeed();
		}

		[Task]
		void ShootTarget()
		{
			if (_target == null)
				return;
			
			var direction = (_target.transform.position - this.transform.position).normalized;
			GetComponent<WeaponSystem>().UsePrimaryWeapon(direction);

			Task.current.Succeed();
		}
		
	}
}

