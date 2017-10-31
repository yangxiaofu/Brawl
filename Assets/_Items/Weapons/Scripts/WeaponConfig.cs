using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	[CreateAssetMenu(menuName = "Game/Weapon")]
	public class WeaponConfig : ItemConfig {
		
		[SerializeField] GameObject _projectilePrefab;
		[SerializeField] float _projectileSpeed = 10f;
		public float projectileSpeed {get{return _projectileSpeed;}}
		[SerializeField] float _secondsBetweenShots = 1f;
		public float secondsBetweenShots {get{return _secondsBetweenShots;}}
		
		public GameObject GetProjectilePrefab()
		{
			return _projectilePrefab;
		}
        public override bool IsWeapon()
        {
            return true;
        }
    }
}

