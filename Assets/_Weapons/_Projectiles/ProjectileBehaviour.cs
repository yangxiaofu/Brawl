using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Core;

namespace Game.Weapons{
	public class ProjectileBehaviour : MonoBehaviour, IProjectile
	{
		[HideInInspector] public Vector3 travelDirection = Vector3.zero;
		[HideInInspector] public Character shootingCharacter;
		protected WeaponConfig _weaponConfig;
		public void Setup(ProjectileBehaviourArgs args)
		{
			shootingCharacter = args.shootingCharacter;
			_weaponConfig 	= args.rangeWeaponConfig;
			travelDirection = args.travelDirection;
		}
        public float GetDamage()
        {
            return _weaponConfig.damageToDeal;
        }

		protected GameObject PlayImpactParticleEffect()
        {
            return Instantiate(_weaponConfig.impactEffect, this.transform.position, this.transform.rotation);
        }
    }
}
