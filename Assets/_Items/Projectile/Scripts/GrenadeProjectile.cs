using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items{
    public class GrenadeProjectile : Projectile
    {
		[Header("Grenade Specfic")]
		[Tooltip("This is the amount of time it takes for the grenade to explode once it hits any collider.")]
		[SerializeField] float _delayBeforeExplosion = 1f;
		[SerializeField] float _explosionRadius = 2f;

        public override void PerformCollisionTasks(Collision other)
        {

            StartCoroutine(BeginParticleEffect(_delayBeforeExplosion));
			StartCoroutine(DestroyProjectileAfter(_secondsToDestroyProjectileAfterCollision));
        }

		IEnumerator BeginParticleEffect(float delay)
		{
			yield return new WaitForSeconds(delay);
			
 			var particleSystemObject = Instantiate(_particleSystemPrefab, this.transform.position, _particleSystemPrefab.transform.rotation) as GameObject;
            particleSystemObject.GetComponent<ParticleSystem>().Play(true);

			Explode();
		}

		private void Explode()
		{
			var hits = Physics.SphereCastAll(this.transform.position, _explosionRadius, Vector3.up);

			for(int i = 0; i < hits.Length; i++)
			{
				var character = hits[i].collider.gameObject.GetComponent<Character>();
				if (character == _character)
					continue;

				if (hits[i].collider.gameObject.GetComponent<Character>())
				{
					AddPointsToShootingCharacter();
					character.GetComponent<HealthSystem>().TakeDamage(_damagePerHit);
				}
					
				
			}
		}
    }
}

