using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items{
	[CreateAssetMenu(menuName = "Game/Projectile/Blast")]
    public class GrenadeProjectile : Projectile
    {	
		void Start () 
		{
			StartCoroutine(DestroyProjectileAfter(_config.timeToDestroy));
		}

        public override void PerformCollisionTasks(Collision other)
        {

            StartCoroutine(PlayParticleEffect((_config as BlastProjectileConfig).delayBeforeExplosion));
			StartCoroutine(DestroyProjectileAfter(_config.secondsToDestroyProjectileAfterCollision));
        }

        public override void Setup(ProjectileArgs args)
        {
			_character = args.character;
            _config = args.config;
        }

        IEnumerator PlayParticleEffect(float delay)
		{
			yield return new WaitForSeconds(delay);
			
 			var particleSystemObject = Instantiate(_config.particleSystemPrefab, this.transform.position, _config.particleSystemPrefab.transform.rotation) as GameObject;
            particleSystemObject.GetComponent<ParticleSystem>().Play(true);

			Explode();
		}

		private void Explode()
		{
			var hits = Physics.SphereCastAll(this.transform.position, (_config as BlastProjectileConfig).blastRadius, Vector3.up);

			for(int i = 0; i < hits.Length; i++)
			{
				var character = hits[i].collider.gameObject.GetComponent<Character>();
				if (character == _character)
					continue;

				if (hits[i].collider.gameObject.GetComponent<Character>())
				{
					AddPointsToShootingCharacter();
					character.GetComponent<HealthSystem>().TakeDamage(GetProjectileDamage());
				}
			}
		}
    }
}

