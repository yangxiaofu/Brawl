using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Core;

namespace Game.Weapons{
	public class ProjectileBehaviour : MonoBehaviour, IDestructable
	{
		[HideInInspector] public Vector3 travelDirection = Vector3.zero;
		[HideInInspector] public Character shootingCharacter;
		WeaponConfig _rangeWeaponConfig;
		public void Setup(ProjectileBehaviourArgs args)
		{
			shootingCharacter = args.shootingCharacter;
			_rangeWeaponConfig 	= args.rangeWeaponConfig;
			travelDirection = args.travelDirection;
		}

        public float GetDamage()
        {
            return _rangeWeaponConfig.damageToDeal;
        }

		void OnTriggerEnter(Collider other)
		{	
			if (other.gameObject.GetComponent<Character>() != shootingCharacter)
			{			
				if (other.gameObject.GetComponent(typeof(IDamageable)))
				{
					var damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
					damageable.DealDamage(_rangeWeaponConfig.damageToDeal);
				} 
				
				var impactEffect = Instantiate(_rangeWeaponConfig.impactEffect, this.transform.position, this.transform.rotation);
				Destroy(this.gameObject);
			}
		}
    }

	public struct ProjectileBehaviourArgs{
		public Character shootingCharacter;
		public Vector3 travelDirection;
		public WeaponConfig rangeWeaponConfig;
		public ProjectileBehaviourArgs(Character shootingCharacter, Vector3 travelDirection, WeaponConfig rangeWeaponConfig)
		{
			this.shootingCharacter = shootingCharacter;
			this.travelDirection = travelDirection;
			this.rangeWeaponConfig = rangeWeaponConfig;
		}
	}
}
