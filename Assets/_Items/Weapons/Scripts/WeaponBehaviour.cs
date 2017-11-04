﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Characters;

namespace Game.Items{
	public class WeaponBehaviour : MonoBehaviour {
		[SerializeField] int _remainingAmmo;
		public int remainingAmmo{get{return _remainingAmmo;}}
		[SerializeField] int _startingAmmo;
		public int startingAmmo {get{return _startingAmmo;}}
		WeaponConfig _config;
		WeaponSystem _weaponSystem;
		
		void Start()
		{
			_weaponSystem = GetComponent<WeaponSystem>();
		}

		public void Setup(WeaponConfig config)
		{
			_config = config;
			_remainingAmmo = config.startingAmmo;
			_startingAmmo = config.startingAmmo;
		}

		public void Fire(Vector3 direction)
		{
			if (_remainingAmmo <= 0) 
				return;
				
			InstantiateProjectile(direction);
			ReduceAmmoBy(1);				
		}

		public void ReduceAmmoBy(int amountToReduce)
		{
			_remainingAmmo -= amountToReduce;
			_remainingAmmo -= 1;
			_remainingAmmo = Mathf.Clamp(_remainingAmmo, 0, _startingAmmo);		
		}

		private void InstantiateProjectile(Vector3 direction)
        {
			if (!_weaponSystem) 
				return;

			if (!_weaponSystem.primaryWeaponBehaviour) 
				return;
			
			if (!_weaponSystem.primaryWeaponBehaviour.GetComponentInChildren<ProjectileSocket>()) 
				return;

            var projectileObject = Instantiate(_config.GetProjectilePrefab()) as GameObject;
			var character = GetComponent<Character>();
			var projectile = projectileObject.GetComponent<Projectile>();
			var particleEffect = _config.GetParticleSystemPrefab();

			var args = new ProjectileArgs(character, direction, particleEffect);
			projectile.Setup(args);

			var socket = _weaponSystem.primaryWeaponBehaviour.GetComponentInChildren<ProjectileSocket>();

			projectileObject.transform.position = new Vector3(
				socket.transform.position.x, 
				socket.transform.position.y, 
				socket.transform.position.z
			);

			var physics = new GamePhysics(direction, _config.projectileSpeed);
			var rigidBody = projectileObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(physics.GetForce(), ForceMode.VelocityChange );
        }
	}

}
