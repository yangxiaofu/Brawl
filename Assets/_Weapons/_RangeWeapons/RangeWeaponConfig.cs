using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Weapons{

	[CreateAssetMenu(menuName = "Game/Range Weapon")]
	public class RangeWeaponConfig : WeaponConfig, IWeaponGrip
	{
		[SerializeField] ProjectileConfig _projectileConfig;
		public ProjectileConfig projectileConfig { get{return _projectileConfig;}}
		[SerializeField] protected int _startingAmmo = 6;
		public int startingAmmo{get{return _startingAmmo;}}
		[SerializeField] float _projectileSpeed = 10f;
		public float projectileSpeed {get{return _projectileSpeed;}}
		[SerializeField] float _secondsBetweenShots = 1f;
		public float secondsBetweenShots {get{return _secondsBetweenShots;}}
		[SerializeField] Transform _weaponGripTransform;
        public Transform weaponGripTransform { get { return _weaponGripTransform; }}
	
		
    }
}

