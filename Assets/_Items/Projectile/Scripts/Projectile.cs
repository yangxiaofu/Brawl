using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Characters;
using Game.Core;

namespace Game.Items{
	public abstract class Projectile : MonoBehaviour 
	{
		[Tooltip("This is the time it takes to destroy the projectile after it has been instantiated in the game scene.")]
		[SerializeField] float _timeToDestroy = 2f;

		[Tooltip("This is the time it takes to destroy the projectile after making a collision with a character.")]		
		[SerializeField] protected float _secondsToDestroyProjectileAfterCollision = 0.2f;
		[SerializeField] protected float _damagePerHit = 10f;

		[Tooltip("This updates the amount of delay that the nav mesh agent has after gettign a force added to it. ")]
		[SerializeField] float _navMeshAgentDelay = 1f;

		[Space]
		[SerializeField] float _projectileCollisionForce = 200f;
		Vector3 _travelDirection = Vector3.zero;
		public float damagePerHit{get{return _damagePerHit;}}
		protected Character _character;
		public Character shootingCharacter{ get{return _character;}}
		protected GameObject _particleSystemPrefab;

		void Start () 
		{
			StartCoroutine(DestroyProjectileAfter(_timeToDestroy));
			
		}

		public void Setup(ProjectileArgs args)
		{
			_character = args.character;
			_travelDirection = args.direction;
			_particleSystemPrefab = args.particleSystemPrefab;
		}

		public void SetDamage(float damage)
		{
			_damagePerHit = damage;
		}

		void OnCollisionEnter(Collision other)
        {
			PerformCollisionTasks(other);
			
        }

		protected void AddPointsToShootingCharacter()
		{
			var scoringSystem = _character.controller.GetComponent<ScoringSystem>();
			var gameMgr = GameManager.Instance();
			scoringSystem.AddPoints(gameMgr.scoreSettings.GetPointsPerHit());
		}

        protected void AddForceToCollidingObject(Collision other)
        {
            var rigidBody = other.gameObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(_travelDirection * _projectileCollisionForce, ForceMode.Impulse);
        }
        protected IEnumerator DestroyProjectileAfter(float delay)
		{
			yield return new WaitForSeconds(delay);
			Destroy(this.gameObject);
		}

		public abstract void PerformCollisionTasks(Collision other);
	}

	public struct ProjectileArgs{
		public Vector3 direction;
		public Character character;
		public GameObject particleSystemPrefab;
		public ProjectileArgs(Character character, Vector3 direction, GameObject particleEffect)
		{
			this.direction = direction;
			this.character = character;
			this.particleSystemPrefab = particleEffect;
		}
	}

}
