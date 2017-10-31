using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

namespace Game.Items{
	public class WeaponBehaviour : MonoBehaviour {

		[SerializeField] int _remainingAmmo;
		public int remainingAmmo{get{return _remainingAmmo;}}
		[SerializeField] int _startingAmmo;
		public int startingAmmo {get{return _startingAmmo;}}
		WeaponConfig _config;
		public void Setup(WeaponConfig config)
		{
			_config = config;
			_remainingAmmo = config.startingAmmo;
			_startingAmmo = config.startingAmmo;
		}
		public void Fire(Vector3 direction, Vector3 socket)
		{
			if (_remainingAmmo <= 0) 
				return;

			InstantiateProjectile(direction, socket);
			ReduceAmmoBy(1);				
		}

		public void ReduceAmmoBy(int amountToReduce)
		{
			_remainingAmmo -= amountToReduce;
			_remainingAmmo -= 1;
			_remainingAmmo = Mathf.Clamp(_remainingAmmo, 0, _startingAmmo);		
		}

		private void InstantiateProjectile(Vector3 direction, Vector3 projectileSocket)
        {
            var projectileObject = Instantiate(
				_config.GetProjectilePrefab(),
				projectileSocket,
				Quaternion.identity
			) as GameObject;

			var physics = new GamePhysics(direction, _config.projectileSpeed);
			var rigidBody = projectileObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(physics.GetForce(), ForceMode.VelocityChange );
        }
	}

}
