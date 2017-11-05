using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items{
    
	public class BulletProjectile : Projectile 
	{
        Vector3 _travelDirection = Vector3.zero;
        void Start () 
		{
			StartCoroutine(DestroyProjectileAfter(_config.timeToDestroy));
		}
        
        public override void PerformCollisionTasks(Collision other)
        {
			if (other.gameObject.GetComponent<Character>() == _character)
				return;

			if (!other.gameObject.GetComponent<Character>())
                return;

            AddPointsToShootingCharacter();
            PlayParticleSystem();
            AddForceToCollidingObject(other);
			StartCoroutine(DestroyProjectileAfter(_config.secondsToDestroyProjectileAfterCollision));
        }

        public override void Setup(ProjectileArgs args)
		{
			_character = args.character;
			_travelDirection = args.direction;
            _config = args.config;
		}

        private void AddForceToCollidingObject(Collision other)
        {
            var rigidBody = other.gameObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(_travelDirection * (_config as BulletProjectileConfig).projectileCollisionForce, ForceMode.Impulse);
        }

        public void PlayParticleSystem()
        {
            var particleSystemObject = Instantiate(_config.particleSystemPrefab, this.transform.position, _config.particleSystemPrefab.transform.rotation) as GameObject;
            particleSystemObject.GetComponent<ParticleSystem>().Play(true);

        }
	}
}


