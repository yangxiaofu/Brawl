using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	public abstract class ProjectileConfig : ScriptableObject {

		[Tooltip("This is the time it takes to destroy the projectile after it has been instantiated in the game scene.")]
		[SerializeField] float _timeToDestroy = 2f;
		public float timeToDestroy{get{return _timeToDestroy;}}

		[Tooltip("This is the time it takes to destroy the projectile after making a collision with a character.")]		
		[SerializeField] float _secondsToDestroyProjectileAfterCollision = 0.2f;
		public float secondsToDestroyProjectileAfterCollision{get{return _secondsToDestroyProjectileAfterCollision;}}


		[Tooltip("This updates the amount of delay that the nav mesh agent has after gettign a force added to it. ")]
		[SerializeField] float _navMeshAgentDelay = 1f;
		[SerializeField] GameObject _projectilePrefab;
		public GameObject GetProjectilePrefab() { return _projectilePrefab;}
		[SerializeField] float _damagePerHit = 10f;
		public float damagePerHit{get{return _damagePerHit;}}

		[SerializeField] GameObject _particleSystemPrefab;
		public GameObject particleSystemPrefab{get{return _particleSystemPrefab;}}

		public abstract void AddComponentTo(GameObject projectileGameObject, ProjectileArgs args);
		
	}

}
