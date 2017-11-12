using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons{

	[CreateAssetMenu(menuName = "Game/Projectile")]
	public class ProjectileConfig : ScriptableObject {

		[Tooltip("This is the time it takes to destroy the projectile after it has been instantiated in the game scene.")]
		[SerializeField] float _timeToDestroy = 2f;
		public float timeToDestroy{get{return _timeToDestroy;}}

		[Tooltip("This is the time it takes to destroy the projectile after making a collision with a character.")]		
		[SerializeField] float _secondsToDestroyProjectileAfterCollision = 0.2f;
		public float secondsToDestroyProjectileAfterCollision{get{return _secondsToDestroyProjectileAfterCollision;}}	
		[SerializeField] GameObject _projectilePrefab;
		public GameObject GetProjectilePrefab() { return _projectilePrefab;}
		[SerializeField] float _damagePerHit = 10f;
		public float damagePerHit{get{return _damagePerHit;}}
		[SerializeField] BlastConfig _blastConfig;
		public BlastConfig blastConfig {get{return _blastConfig;}}
		
	}

}
