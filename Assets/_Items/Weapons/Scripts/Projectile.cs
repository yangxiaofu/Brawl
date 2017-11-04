using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Characters;

namespace Game.Items{
	public class Projectile : MonoBehaviour 
	{
		[Tooltip("This is the time it takes to destroy the projectile after it has been instantiated in the game scene.")]
		[SerializeField] float _timeToDestroy = 2f;

		[Tooltip("This is the time it takes to destroy the projectile after making a collision with a character.")]		
		[SerializeField] float _secondsToDestroyProjectileAfterCollision = 0.2f;
		[SerializeField] float _damagePerHit = 10f;

		[Tooltip("This updates the amount of delay that the nav mesh agent has after gettign a force added to it. ")]
		[SerializeField] float _navMeshAgentDelay = 1f;

		[Space]
		[SerializeField] float _projectileCollisionForce = 200f;
		Vector3 _travelDirection = Vector3.zero;
		public float damagePerHit{get{return _damagePerHit;}}
		Character _character;
		public Character shootingCharacter{ get{return _character;}}
		GameObject _particleSystemPrefab;

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
			if (other.gameObject.GetComponent<Character>() == _character)
				return;

			if (!other.gameObject.GetComponent<Character>())
				return;

			var particleSystemObject = Instantiate(_particleSystemPrefab, this.transform.position, _particleSystemPrefab.transform.rotation) as GameObject;
			particleSystemObject.GetComponent<ParticleSystem>().Play(true);
			
			var rigidBody = other.gameObject.GetComponent<Rigidbody>();
			rigidBody.AddForce(_travelDirection * _projectileCollisionForce, ForceMode.Impulse);	
			StartCoroutine(DestroyProjectileAfter(_secondsToDestroyProjectileAfterCollision));
            
        }
        IEnumerator DestroyProjectileAfter(float delay)
		{
			yield return new WaitForSeconds(delay);
			Destroy(this.gameObject);
		}
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
