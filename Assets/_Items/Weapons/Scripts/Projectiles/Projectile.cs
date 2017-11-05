using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Game.Characters;

using Game.Core;

namespace Game.Items{
	public abstract class Projectile : MonoBehaviour 
	{
		protected ProjectileConfig _config;
		protected Character _character;
		public Character shootingCharacter{ get{return _character;}}
		public float GetProjectileDamage(){
			return _config.damagePerHit;
		}
		void OnCollisionEnter(Collision other)
        {
			PerformCollisionTasks(other);
        }

		protected void AddPointsToShootingCharacter()
		{
			Assert.IsNotNull(_character, "You may have forgotten it in the setup method.");
			var scoringSystem = _character.controller.GetComponent<ScoringSystem>();
			var gameMgr = GameManager.Instance();
			scoringSystem.AddPoints(gameMgr.scoreSettings.GetPointsPerHit());
		}

        protected IEnumerator DestroyProjectileAfter(float delay)
		{
			yield return new WaitForSeconds(delay);
			Destroy(this.gameObject);
		}

		public abstract void PerformCollisionTasks(Collision other);
		public abstract void Setup(ProjectileArgs args);
	}

	public struct ProjectileArgs{
		public Vector3 direction;
		public Character character;

		public ProjectileConfig config;

		public ProjectileArgs(Character character, Vector3 direction, ProjectileConfig config)
		{
			this.direction = direction;
			this.character = character;
			this.config = config;
		}
	}

}
