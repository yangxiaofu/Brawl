using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Panda;
using Game.Items;

namespace Game.Characters
{
	public class Enemy : Character
	{
		[Header("Enemy Specific")]
		[Tooltip("This is the distance in which the enemy will stop in front of the player and start shooting. ")]
		[SerializeField] float _maxShootingDistance = 10f;
		[Space]
		[Header("Danger Zone")]
		[Tooltip("When the amount goes below this, the enemy will run and hide for cover until he has enough health.")]
		[Range(0, 1)]
		[SerializeField] float _inDangerThreshold = 0.2f;
		[Tooltip("Setting this will make the enemy go from hiding to attacking an enemy.")]
		[Range(0, 1)]
		[SerializeField] float _beginAttackThreshold = 0.8f;


		List<Character> _characters = new List<Character>();
		Character _target;
		NavMeshAgent _agent;
		bool _isAttacking = false;
		
        void Start()
		{
			InitializeVariables();
			//Finds all characters and removes self.
			_characters = FindObjectsOfType<Character>().ToList();
			_characters.Remove(this);

			_agent = GetComponent<NavMeshAgent>();
			_agent.updatePosition = false;

			Assert.IsNotNull(_agent);
		}

		void Update()
		{
			this.transform.position = new Vector3
			(
				_agent.nextPosition.x, 
				this.transform.position.y, 
				_agent.nextPosition.z
			);
		}

		public override void OnCollisionEnterAction(Collision other)
        {
			//TODO: Do Ragdoll effect on death.
        }

		[Task] bool IsInDanger()
		{
			return GetComponent<HealthSystem>().healthAsPercentage < _inDangerThreshold;
		}

		[Task] 
		bool IsAttacking ()
		{
			return _isAttacking;
		}

		[Task]
		bool DoCharactersExist()
		{
			for(int i = 0; i < _characters.Count; i++)
			{
				if (!_characters[i].IsDead()){
					return true;
				}
			}

			return false;
		}

		[Task]
		bool StopAttacking()
		{
			_isAttacking = false;
			return true;
		}

		[Task]
		void ChooseRandomTarget()
		{
			var r = UnityEngine.Random.Range(0, _characters.Count);

			if (_target != null)
				_target.SetBeingAttacked(false);

			_target = _characters[r];
			_target.SetBeingAttacked(true);
			
			Task.current.Succeed();
		}

		[Task]
		void MoveTowardTarget()
		{
			_agent.isStopped = false;
			_agent.SetDestination(_target.transform.position);

			SetIsAttacking();

			if (_agent.remainingDistance <= _agent.stoppingDistance)
			{	
				Task.current.Succeed();
			}	
		}

		[Task]
		bool TargetIsInAttackDistance()
		{
			return Vector3.Distance(
				_target.transform.position, 
				this.transform.position
			) < _maxShootingDistance;
		}

		[Task]
		bool SetIsAttacking()
		{
			if (_agent.remainingDistance <= _maxShootingDistance)
			{
				_isAttacking = true;
			}
			return true;
		}

		[Task]
		bool IsBeingAttack()
		{
			return true;
		}

		[Task] 
		bool ReadyToFight()
		{
			return GetComponent<HealthSystem>().healthAsPercentage > _beginAttackThreshold;
		}


		[Task]
		void MoveToRandomSpot()
		{
			var xRandom = UnityEngine.Random.Range(-100, 100);
			var yRandom = UnityEngine.Random.Range(-100, 100);
			var destination = new Vector3(xRandom, 0, yRandom);
			_agent.SetDestination(destination);
			Task.current.Succeed();
		}

		[Task]
		void Stop()
		{
			_agent.isStopped = true;
			_agent.SetDestination(this.transform.position);
			Task.current.Succeed();
		}

		
		float timer = 0;
		[Task]
		void ShootTarget()
		{
			if (Task.current.isStarting)
			{
			    timer = 0;
			}

			timer += Time.deltaTime;
		
			if (timer >= _weaponSystem.GetPrimaryWeapon().secondsBetweenShots)
			{
				var direction = (_target.transform.position - this.transform.position).normalized;
				ShootProjectile(direction);
				Task.current.Succeed();
			}
		}

		[Task] bool IsTargetWithinShootDistance()
		{
			var distanceFromTarget = Vector3.Distance(
				this.transform.position, 
				_target.transform.position
			);

			return distanceFromTarget < _maxShootingDistance;
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(this.transform.position, _maxShootingDistance);
		}
	}
}
