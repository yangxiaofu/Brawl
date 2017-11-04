using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items{
	public class BulletProjectile : Projectile 
	{
        public override void PerformCollisionTasks(Collision other)
        {
			if (other.gameObject.GetComponent<Character>() == _character)
				return;

			if (!other.gameObject.GetComponent<Character>())
                return;

            AddPointsToShootingCharacter();
            PlayParticleSystem();
            AddForceToCollidingObject(other);
			StartCoroutine(DestroyProjectileAfter(_secondsToDestroyProjectileAfterCollision));
        }

        public void PlayParticleSystem()
        {
            var particleSystemObject = Instantiate(_particleSystemPrefab, this.transform.position, _particleSystemPrefab.transform.rotation) as GameObject;
            particleSystemObject.GetComponent<ParticleSystem>().Play(true);

        }
	}
}


