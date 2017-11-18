using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Weapons{

	[CreateAssetMenu(menuName = "Game/Range Weapon")]
	public class RangeWeaponConfig : WeaponConfig, IWeaponGrip
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
		[SerializeField] Transform _weaponGripTransform;
        public Transform weaponGripTransform
        {
            get
            {
                return _weaponGripTransform;
			}
        }

		[SerializeField] float _damageToDeal = 25;

        public void AddComponentTo(GameObject projectileGameObject, Character character, Vector3 direction)
		{
			var behaviour = _projectileConfig.blastConfig.AddComponentTo(projectileGameObject);
			behaviour.Setup(this, character);

			//Adds the sphere collider. 
			var sphereCollider = projectileGameObject.AddComponent<SphereCollider>();
			sphereCollider.isTrigger = false;

			//Adds the direction of velocity
			var bulletBehaviour = projectileGameObject.AddComponent<ProjectileBehaviour>();
			bulletBehaviour.Setup(_damageToDeal);
			bulletBehaviour.shootingCharacter = character;
			bulletBehaviour.travelDirection = direction;
		}
        public override BlastConfig GetBlastConfig()
        {
            return _projectileConfig.blastConfig;
        }

        public override float GetBlastDelayAfterCollision()
        {
            return _projectileConfig.blastConfig.delayBeforeExplosion;
        }
    }
}

