using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;
using System;

namespace Game.Weapons
{
	public class PowerProjectileBehaviour : ProjectileBehaviour 
	{
		void OnTriggerEnter(Collider other)
        {
            if (IsShootingCharacter(other.gameObject.GetComponent<Character>()))
                return;

            RaycastHit[] hits = GetHitsOnExplosion();
            DealDamageTo(hits);

            var args = new HittableArgs(
                hits, 
                this.transform.position, 
                shootingCharacter, 
                _weaponConfig
            );

            var hittableCharacters = new HittableCharacters(args);
            var bodies = new GamePhysics(hittableCharacters);
            bodies.ApplyForce();
           
            var hittableObjects = new HittableObjects(args);
            var otherObjects = new GamePhysics(hittableObjects);
            otherObjects.ApplyForce();

            AddDestroyTimerToParticleEffectObject();
            Destroy(this.gameObject);
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

        private void DealDamageTo(RaycastHit[] hits)
        {
            for (int i = 0; i < hits.Length; i++)
            { 
                var hitCharacter = hits[i].collider.gameObject.GetComponent<Character>();  

                if (!IsShootingCharacter(hitCharacter)){
                    var damageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                    if (damageable == null)
                        continue;

                    damageable.DealDamage(_weaponConfig.damageToDeal);
                }
            }
        }
    }
}
