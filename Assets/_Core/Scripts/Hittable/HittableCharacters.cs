using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Weapons;

namespace Game.Core{
	public class HittableCharacters : Hittable, IHittable
    {
		public HittableCharacters(HittableArgs args){
			_hits = args.hits;
			_explosionPosition = args.explosionPosition;
			_shootingCharacter = args.shootingCharacter;
			_weaponConfig = args.weaponConfig;
		}
        public void ApplyForce()
        {
			for(int i = 0; i < _hits.Length; i++)
			{ 
				var enemyObject = _hits[i].collider.gameObject.GetComponent<Enemy>();
				if (enemyObject && enemyObject.GetComponent<Enemy>().isDead)
				{
					float forceFactor = 0.5f;
					var direction = (_hits[i].collider.gameObject.transform.position - _explosionPosition).normalized;
					var forceDirection = new Vector3(direction.x, forceFactor, direction.z);
					enemyObject.GetComponent<Rigidbody>().AddForce(forceDirection * (_weaponConfig as PowerWeaponConfig).explosionForceFactor, ForceMode.Impulse);
				}
			
			}
        }
    }
}
