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
			if (IsShootingCharacter(other.gameObject.GetComponent<Character>()))
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

