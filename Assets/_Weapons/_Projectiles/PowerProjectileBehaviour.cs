using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;
using System;

namespace Game.Weapons
{
	//KNOWN BUGS.  The enemies explode all in the same direction for some reason. 
	public class PowerProjectileBehaviour : ProjectileBehaviour 
	{
		void OnTriggerEnter(Collider other)
		{	
			if (other.gameObject.GetComponent<Character>() != shootingCharacter)
            {
                RaycastHit[] hits = GetHitsOnExplosion();
                DealDamageTo(hits);
                AddForceToDeadHits(hits);
                PlayImpactParticleEffect();
                Destroy(this.gameObject);
            }
        }

        private RaycastHit[] GetHitsOnExplosion()
        {
            float explosionRadius = (_weaponConfig as PowerWeaponConfig).GetBlastRadius();

            RaycastHit[] hits = Physics.SphereCastAll(
                this.transform.position,
                explosionRadius,
                Vector3.up,
                explosionRadius
            );
            return hits;
        }

        private void AddForceToDeadHits(RaycastHit[] hits)
        {
            for(int i = 0; i < hits.Length; i++)
			{
				var enemyObject = hits[i].collider.gameObject.GetComponent<Enemy>();
				if (enemyObject && enemyObject.GetComponent<Enemy>().isDead)
				{
					float forceFactor = 0.5f;
					var direction = (hits[i].collider.gameObject.transform.position - this.transform.position).normalized;
					var forceDirection = new Vector3(direction.x, forceFactor, direction.z);
					enemyObject.GetComponent<Rigidbody>().AddForce(forceDirection * (_weaponConfig as PowerWeaponConfig).explosionForceFactor, ForceMode.Impulse);
				}
			}
        }

        private void DealDamageTo(RaycastHit[] hits)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                var damageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                if (damageable == null)
                    continue;

                damageable.DealDamage(_weaponConfig.damageToDeal);
            }
        }
    }
}
