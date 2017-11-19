using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using System;

namespace Game.Weapons
{
    //KNOWN BUGS: UPDATE THE CHARGE  RELATIE TO THE MAX CHARGE ON THE BLAST.  THIS SHOULD BE SCALED. 
    //KNOWN BUGS: THE CHARACTER DOES NOT MOVE WHEN THE BLAST EXPLOSION OCCURS.
	public class PowerWeaponBehaviour : WeaponBehaviour	{
		float _currentCharge = 0;
		float _maxCharge = 100f;
		PowerWeaponSocket _socket;
		bool _chargingAllowed = false;
		bool _released = false;
		GameObject _projectileObject;
        EnergySystem _energySystem;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
		public void Setup(PowerWeaponConfig config)
		{
			_config = config;

			_socket = GetComponentInChildren<PowerWeaponSocket>();
			Assert.IsNotNull(_socket, "There is no PowerWeaponSocket in the child of " + this.gameObject.name);

            _energySystem = GetComponent<EnergySystem>();
            Assert.IsNotNull(_energySystem, "An energy System component needs to be added to " + this.gameObject.name);
		}

		void Update()
        {
            if (_chargingAllowed)
            {
                var energyRemaining = _energySystem.ConsumeEnergy((_config as PowerWeaponConfig).energyConsumptionPerSecond);
                if (energyRemaining > 0)
                {
                    ContinueChargingPowerWeapon();
                    MakeProjectileBigger();
                }
            }

            KeepProjectileInSocket();
        }

        private void KeepProjectileInSocket()
        {
            if (_projectileObject && !_released)
            {
                _projectileObject.transform.position = _socket.transform.position;
            }
        }

        private void MakeProjectileBigger()
        {
            _projectileObject.transform.localScale *= (_config as PowerWeaponConfig).projectileIncreasePerSecondOnCharge;
        }

        private void ContinueChargingPowerWeapon()
        {
            _currentCharge += (_config as PowerWeaponConfig).chargePerSecond * Time.deltaTime;
            _currentCharge = Mathf.Clamp(_currentCharge, 0, _maxCharge);
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

            AddForceToProjectile();
            PlayGunFireAudio();
            
            var behaviour = _projectileObject.AddComponent<ProjectileBehaviour>();
            
            var args = new ProjectileBehaviourArgs(
                GetComponent<Character>(),
                this.transform.forward * (_config as PowerWeaponConfig).forceFacter * _currentCharge, 
                _config
            );

            behaviour.Setup(args);

            ResetPowerWeaponBehaviour();
        }

        private void AddForceToProjectile()
        {
			Vector3 forceToAdd = (this.transform.forward * _currentCharge * (_config as PowerWeaponConfig).forceFacter);
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
