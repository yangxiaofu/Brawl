using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items{
	[CreateAssetMenu(menuName = "Game/Weapon")]
	public class WeaponConfig : ItemConfig {
		[SerializeField] GameObject _projectilePrefab;
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
            return true;
        }
    }
}

