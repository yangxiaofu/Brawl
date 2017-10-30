using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Panda;
using Game.Weapons;

namespace Game.Characters
{
	public class Enemy : Character
	{
		[SerializeField] List<Character> _characters = new List<Character>();
		[SerializeField] Character _target;
		[SerializeField] float _shootDistance = 10f;
		NavMeshAgent _agent;
		bool _isAttacking = false;
        void Start()
		{
			InitializeVariables();
			//Finds all characters and removes self.
			_characters = FindObjectsOfType<Character>().ToList();
			_characters.Remove(this);

			_agent = GetComponent<NavMeshAgent>();
			Assert.IsNotNull(_agent);
		}

		public override void OnCollisionEnterAction(Collision other)
        {
            //TODO: Do Ragdoll effect on collision.
        }

		[Task] 
		bool IsAttacking ()
		{
			return _isAttacking;
		}

		[Task]
		bool DoesCharacterExist()
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
		void StopAttacking()
		{
			_isAttacking = false;
		}

		[Task]
		void ChooseRandomCharacter()
		{
			var r = UnityEngine.Random.Range(0, _characters.Count);
			_target = _characters[r];
			Task.current.Succeed();
		}

		[Task]
		void MoveTowardCharacter()
		{
			_agent.isStopped = false;
			_agent.SetDestination(_target.transform.position);

			if (_agent.remainingDistance <= _shootDistance)
			{	
				_isAttacking = true;
				Task.current.Succeed();
			}	
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
			var distanceFromTarget = Vector3.Distance(this.transform.position, _target.transform.position);
			return distanceFromTarget < _shootDistance;
		}
	}
}
