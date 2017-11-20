using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;

namespace Game.Weapons
{
	public class RangeProjectileBehaviour : ProjectileBehaviour{
		void OnTriggerEnter(Collider other)
        {
            CreateProjectileImpactEffectsOn(other);
        }

        private void CreateProjectileImpactEffectsOn(Collider other)
        {
            //Checks Character component on the hierarchy or the parent hierarchy.  It may collide with a collider on a character object where the Character Component is not at ths same hierarchy. If it is the shooting character, it will not disappear. 
            if (IsShootingCharacter(other.gameObject.GetComponent<Character>()) || IsShootingCharacter(other.gameObject.GetComponentInParent<Character>()))
                return;

            if (other.gameObject.GetComponent(typeof(IDamageable)))
            {
                var damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
                damageable.DealDamage(_weaponConfig.damageToDeal);
            }

            AddDestroyTimerToParticleEffectObject();
            Destroy(this.gameObject);
        }
    }
}

