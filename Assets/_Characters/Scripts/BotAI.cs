using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;
using Game.Items;
using UnityEngine.Assertions;

namespace Game.Characters{
	public class BotAI : MonoBehaviour {
		List<Character> _characters = new List<Character>();
		Character _character;
		Character _target;
		NavMeshAgent _agent;
		WeaponSystem _weaponSystem;

		void Start()
		{
			//TODO: Change to Type Get when all merged.
			_character = GetComponent<Character>();

			_characters = FindObjectsOfType<Character>().ToList();
			_characters.Remove(_character);

			_weaponSystem = GetComponent<WeaponSystem>();
			Assert.IsNotNull(_weaponSystem);

			_agent = GetComponent<NavMeshAgent>();
			
		}

		void Update()
        {
            if (!_character.isBot) return;

            UpdateBotMovement();
        }

        private void UpdateBotMovement()
        {
            this.transform.position = new Vector3
            (
                _agent.nextPosition.x,
                this.transform.position.y,
                _agent.nextPosition.z
            );
        }

		[Task] bool Jump()
		{
			return _character.Jump();
		}

		[Task] bool Dash()
		{
			return _character.Dash();
		}

        [Task] bool IsBot()
		{
			return _character.isBot;
		}

		[Task] bool IsInDanger()
		{
			return GetComponent<HealthSystem>().healthAsPercentage < _character.inDangerThreshold;
		}


		[Task] 
		bool IsAttacking ()
		{
			return _character.isAttacking;
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
			_character.isAttacking = false;
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
			) < _character.maxShootingDistance;
		}

		[Task]
		bool SetIsAttacking()
		{
			if (_agent.remainingDistance <= _character.maxShootingDistance)
			{
				_character.isAttacking = true;
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
			return GetComponent<HealthSystem>().healthAsPercentage > _character.beginAttackThreshold;
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
		
			if (timer >= _weaponSystem.primaryWeapon.secondsBetweenShots)
			{
				var direction = (_target.transform.position - this.transform.position).normalized;

				_weaponSystem.ShootProjectile(
					direction, 
					_character.projectileSocket.transform.position
				);
				
				Task.current.Succeed();
			}
		}

		[Task] bool IsTargetWithinShootDistance()
		{
			var distanceFromTarget = Vector3.Distance(
				this.transform.position, 
				_target.transform.position
			);

			return distanceFromTarget < _character.maxShootingDistance;
		}
	}

}
