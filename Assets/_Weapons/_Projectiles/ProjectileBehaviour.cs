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
		RangeWeaponConfig _rangeWeaponConfig;
		float _damageToDeal = 25f;
		public void Setup(ProjectileBehaviourArgs args)
		{
			_damageToDeal = args.damageToDeal;
			shootingCharacter = args.shootingCharacter;
			_rangeWeaponConfig 	= args.rangeWeaponConfig;
			travelDirection = args.travelDirection;
		}

        public float GetDamage()
        {
            return _damageToDeal;
        }

		void OnTriggerEnter(Collider other)
		{	
			if (other.gameObject.GetComponent<Character>() == shootingCharacter)
				return;
			
			if (other.gameObject.GetComponent(typeof(IDamageable)))
			{
				var damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
				damageable.DealDamage(_damageToDeal);
			}

			var impactEffect = Instantiate(_rangeWeaponConfig.impactEffect, this.transform.position, this.transform.rotation);
			
			Destroy(this.gameObject);
		}
    }

	public struct ProjectileBehaviourArgs{
		public float damageToDeal;
		public Character shootingCharacter;
		public Vector3 travelDirection;

		public RangeWeaponConfig rangeWeaponConfig;
		public ProjectileBehaviourArgs(float damageToDeal, Character shootingCharacter, Vector3 travelDirection, RangeWeaponConfig rangeWeaponConfig)
		{
			this.damageToDeal = damageToDeal;
			this.shootingCharacter = shootingCharacter;
			this.travelDirection = travelDirection;
			this.rangeWeaponConfig = rangeWeaponConfig;
		}
	}
}
