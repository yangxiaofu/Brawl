using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Characters;

namespace Game.Weapons
{
	public class RangeWeaponBehaviour : WeaponBehaviour
	{
		[SerializeField] int _remainingAmmo;
		public int remainingAmmo{get{return _remainingAmmo;}}
		[SerializeField] int _startingAmmo;

		void Start()
		{
			_weaponSystem = GetComponent<WeaponSystem>();
			_audioSource = GetComponent<AudioSource>();
		}

		///<summary> Used Primary for testing purposes</summary>
		public void Setup(int remainingAmmo, int startingAmmo) 
		{
			_remainingAmmo = remainingAmmo;
			_startingAmmo = startingAmmo;
		}

		public void Setup(WeaponConfig config)
		{
			_config = config;
			_remainingAmmo = (config as RangeWeaponConfig).startingAmmo;
			_startingAmmo = (config as RangeWeaponConfig).startingAmmo;
		}

		public void Fire(Vector3 direction)
        {
            if (_remainingAmmo <= 0)
                return;

            InstantiateProjectile(direction);
            PlayGunFireAudio();
            ReduceAmmoBy(1);
        }

      

        public void ReduceAmmoBy(int amountToReduce)
		{
			_remainingAmmo -= amountToReduce;
			_remainingAmmo = Mathf.Clamp(_remainingAmmo, 0, _startingAmmo);		
		}

		public void IncreaseAmmo(int increaseAmmo)
		{
			_remainingAmmo += increaseAmmo;
			_remainingAmmo = Mathf.Clamp(_remainingAmmo, 0, _startingAmmo);
		}

		private void InstantiateProjectile(Vector3 direction)
        {
			if (!_weaponSystem) 
				Debug.LogError("There is not weapon system.");

			if (!_weaponSystem.primaryWeaponBehaviour) 
				Debug.LogError("You need to have a Primary Weapon Behaviour");
			
			if (!_weaponSystem.primaryWeaponBehaviour.GetComponentInChildren<ProjectileSocket>())
				Debug.LogError("There is no projectile socket in the child of " + this.gameObject);

			var rangeWeaponConfig = (_config as RangeWeaponConfig);
            var projectileObject = Instantiate(rangeWeaponConfig.projectileConfig.GetProjectilePrefab()) as GameObject;

			var sphereCollider = projectileObject.AddComponent<SphereCollider>();
			sphereCollider.isTrigger = false;
			
			var projectileBehaviour = projectileObject.AddComponent<RangeProjectileBehaviour>();

			var args = new ProjectileBehaviourArgs(
				GetComponent<Character>(), 
				direction, 
				_config as RangeWeaponConfig
			);
			projectileBehaviour.Setup(args);

			var socket = _weaponSystem.primaryWeaponBehaviour.GetComponentInChildren<ProjectileSocket>();

			projectileObject.transform.position = new Vector3(
				socket.transform.position.x, 
				socket.transform.position.y, 
				socket.transform.position.z
			);

			var physics = new GamePhysics(direction, (_config as RangeWeaponConfig).projectileSpeed);
			var rigidBody = projectileObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(physics.GetForce(), ForceMode.VelocityChange );

			
			
        }
	}
}

