    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.Weapons
{
	public class PowerWeaponBehaviour : MonoBehaviour 
	{
		PowerWeaponConfig _config;
		float _currentCharge = 0;
		float _maxCharge = 100f;
		PowerWeaponSocket _socket;
		bool _chargingAllowed = false;
		bool _released = false;
		GameObject _projectileObject;
        EnergySystem _energySystem;
        PowerWeaponBehaviourLogic _logic;
        
		
		public void Setup(PowerWeaponConfig config)
		{
			_config = config;

			_socket = GetComponentInChildren<PowerWeaponSocket>();
			Assert.IsNotNull(_socket, "There is no weapon grip transform in the child of " + this.gameObject.name);

            _energySystem = GetComponent<EnergySystem>();

            _logic = new PowerWeaponBehaviourLogic();
		}

		void Update()
		{
			if (_logic.CanCharge(_chargingAllowed, _energySystem.HasEnergy()))
            {
                ContinueCharge();
            }

            if (_projectileObject && !_released)
			{
				_projectileObject.transform.position = _socket.transform.position;
			}
		}

        private void ContinueCharge()
        {
            var energyRemaining = _energySystem.ConsumeEnergy(_config.energyConsumptionPerSecond);
            if (energyRemaining > 0)
            {
                 _currentCharge += _config.chargePerSecond * Time.deltaTime;
                _currentCharge = Mathf.Clamp(_currentCharge, 0, _maxCharge);
            } 
        }

        public void StartCharging()
        {
            _chargingAllowed = true;
            _released = false;
            var prefab = _config.GetItemPrefab();
            _projectileObject = Instantiate(prefab, _socket.transform.position, Quaternion.identity) as GameObject;
			
            SetupRigidBody();
        }

        private void SetupRigidBody()
        {
            var rb = _projectileObject.GetComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.useGravity = false;
        }

        public void Use()
        {
            if (!_projectileObject) //There's a possible error here? 
                return;

            SetupProjectile();
            AddForceToProjectile();
            ResetPowerWeaponBehaviour();
        }

        private void SetupProjectile()
        {
			if (_projectileObject == null) 
				Debug.LogError("Projectile Object should not be null.");

			var collider = _projectileObject.AddComponent<SphereCollider>();
            collider.isTrigger = false;
			var behaviour = _config.GetBlastConfig().AddComponentTo(_projectileObject);
			behaviour.Setup(_config, GetComponent<Characters.Character>());
        }

        private void AddForceToProjectile()
        {
			Vector3 forceToAdd = (this.transform.forward * _currentCharge * _config.forceFacter);
            _projectileObject.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);
        }

        private void ResetPowerWeaponBehaviour()
        {
            _released = true;
            _chargingAllowed = false;
            _currentCharge = 0;
			_projectileObject = null;
        }
    }
}
