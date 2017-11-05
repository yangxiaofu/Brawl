using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items{
	[CreateAssetMenu(menuName = "Game/Range Weapon")]
	public class RangeWeaponConfig : WeaponConfig 
	{
		[SerializeField] ProjectileConfig _projectileConfig;
		public ProjectileConfig projectileConfig{
			get{return _projectileConfig;}
		}
		[SerializeField] protected int _startingAmmo = 6;
		public int startingAmmo{get{return _startingAmmo;}}
		[SerializeField] float _projectileSpeed = 10f;
		public float projectileSpeed {get{return _projectileSpeed;}}
		[SerializeField] float _secondsBetweenShots = 1f;
		public float secondsBetweenShots {get{return _secondsBetweenShots;}}
		public void AddComponentTo(GameObject projectileGameObject, Character character, Vector3 direction)
		{
			var args = new ProjectileArgs(character, direction, _projectileConfig);
			_projectileConfig.AddComponentTo(projectileGameObject, args);
		}

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}

