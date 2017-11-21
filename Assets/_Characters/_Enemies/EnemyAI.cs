using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Weapons;
using Panda;

namespace Game.Characters{
	public class EnemyAI : MonoBehaviour {
		[SerializeField] float _rotationSpeed = 5f;

		[Tooltip("Adjusting this will adjust the max distance the enemy will run away from the player when the shooter is too close.  This only applies to shooter enemies who run.")]
		[SerializeField] float _targetPositionRangeOnRun = 100f;
		Enemy _enemy;
		NavMeshAgent _agent;
		Animator _anim;
		const string IS_WALKING = "IsWalking";
		const string ATTACK = "Attack";
		List<MyPlayer> _players = new List<MyPlayer>();
		MyPlayer _target;
		Vector3 _targetedPos;
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
		void PickPositionAwayFromPlayer()
		{
			if (_target == null)
			{
				Task.current.Fail();
				return;
			}

			

			var randomPoint = new Vector3
			(
				this.transform.position.x + UnityEngine.Random.Range(-_targetPositionRangeOnRun, _targetPositionRangeOnRun),
				0, 
				this.transform.position.z + UnityEngine.Random.Range(-_targetPositionRangeOnRun, _targetPositionRangeOnRun)
			);

			NavMeshHit hit;
			if(NavMesh.SamplePosition(randomPoint, out hit, _targetPositionRangeOnRun, NavMesh.AllAreas))
			{
				_targetedPos = hit.position;

				var distance = Vector3.Distance(_target.transform.position, _targetedPos);

				if (distance > _enemy.runawayDistance)
				{
					Task.current.Succeed();
				}
			} 
		}

		[Task]
		void MoveToTargetedPosition()
		{
			_agent.SetDestination(_targetedPos);

			if (_target) //If there's a targetd player.
			{
				var distanceFromTarget = Vector3.Distance(this.transform.position, _target.transform.position);

				if (Task.isInspected)
					Task.current.debugInfo = string.Format(
						"Distance From Target is {0}.  The distance from the target Position is {1}", 
						distanceFromTarget,
						Vector3.Distance(this.transform.position, _targetedPos)
					);
				
				if (distanceFromTarget > _enemy.runawayDistance)  //If player is far enough away, then stop the enemy.
				{
					_agent.SetDestination(this.transform.position);
					Task.current.Succeed();
				} //otherwise, continue to move to the destination.
			} 
			else //If no targeted player, this will just move to the targeted position until it gets there. 
			{
				if (_agent.remainingDistance <= _agent.stoppingDistance)
				{
					Task.current.Succeed();
				} 
			}	
		}

		[Task]
		bool HasTargetedPlayer()
		{
			return _target != null;
		}

		[Task]
		void FaceTarget()
		{
			if (_target == null)
			{
				Task.current.Fail();
				return;
			}

			Vector3 direction = (_target.transform.position - this.transform.position).normalized;

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeed);

			float mininalAngle = 5f;
			if (Vector3.Angle(this.transform.forward, direction) < mininalAngle)
			{
				Task.current.Succeed();
			}

		}

		[Task]
		bool PlayerIsOutsideOfRunRadius()
		{
			if (_target == null)
				return false;

			var distance = Vector3.Distance(this.transform.position, _target.transform.position);
			return distance >= _enemy.runawayDistance;
		}

		[Task]
		bool CanSeeTarget()
		{
			if (_target == null)
				return false;

			Vector3 direction = (_target.transform.position - this.transform.position).normalized;
			RaycastHit hit;
			if (Physics.Raycast(this.transform.position, direction, out hit, _enemy.moveRadius))
			{
				if (hit.collider.gameObject.GetComponent<MyPlayer>() == _target)
				{
					return true;
				}
			}

			return false;			
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
		bool MoveToPlayer()
		{
			if (_target == null)
				return false;

			if (_agent.enabled)
			{
				_agent.SetDestination(_target.transform.position);
				return true;
			} 
			else 
			{
				return false;
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

