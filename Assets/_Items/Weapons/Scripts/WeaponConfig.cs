using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using System;

namespace Game.Items{
	[CreateAssetMenu(menuName = "Game/Weapon")]
	public class WeaponConfig : ItemConfig {
		[SerializeField] bool _isWeapon = false;
		[SerializeField] GameObject _projectilePrefab;
		[SerializeField] Transform _weaponGripTransform;
		public Transform weaponGripTransform{get{return _weaponGripTransform;}}

		[Space]
		[SerializeField] int _startingAmmo = 6;
		public int startingAmmo{get{return _startingAmmo;}}
		[SerializeField] float _projectileSpeed = 10f;
		public float projectileSpeed {get{return _projectileSpeed;}}
		[SerializeField] float _secondsBetweenShots = 1f;
		public float secondsBetweenShots {get{return _secondsBetweenShots;}}
		
		[SerializeField] float _damagePerHit = 10f;
		public GameObject GetProjectilePrefab()
		{
			_projectilePrefab.GetComponent<Projectile>().SetDamage(_damagePerHit);
			return _projectilePrefab;
		}
        public override bool IsWeapon()
        {
            return _isWeapon;
        }
    }
}

