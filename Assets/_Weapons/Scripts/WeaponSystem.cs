using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Core;
using Game.Core.ControllerInputs;
using Panda;
using System;

namespace Game.Weapons{
	public class WeaponSystem : MonoBehaviour {
		[SerializeField] WeaponConfig _primaryWeapon;
		public WeaponConfig primaryWeapon{get{return _primaryWeapon;}}
		[SerializeField] PowerWeaponConfig _powerWeaponConfig;
		[SerializeField] SpecialAbilityConfig _specialAbilty;
		[SerializeField] float _lowAmmoThreshold = 1; //TODO: REfactor out into the weapon beahviour later. 

		[Tooltip("This is the minimum percentage of maximum energy that allows you to charge your power weapon.")]
		[SerializeField] float _thresholdToAllowChargeOfPowerWeawpon = 0.4f;
		[SerializeField] float _ammoAutoIncreasePerSecond = 1;
		float rightAnalogStickThreshold = 0.9f;
		WeaponBehaviour _primaryWeaponBehaviour;
		public WeaponBehaviour primaryWeaponBehaviour{get{return _primaryWeaponBehaviour;}}
		WeaponGrip[] _weaponGrip;
		ThrowSocket _throwSocket;
		EnergySystem _energySystem;
		WeaponSystemLogic _weaponSystemLogic;
		PowerWeaponBehaviour _powerWeaponBehaviour;
		
		void Start()
		{
			_throwSocket = GetComponentInChildren<ThrowSocket>();
			Assert.IsNotNull(_throwSocket, "Please add a throw socket game object to the child of " + this.gameObject.name);
			
			_energySystem = GetComponent<EnergySystem>();
			Assert.IsNotNull(_energySystem);

			_weaponSystemLogic = new WeaponSystemLogic();

			_powerWeaponBehaviour = _powerWeaponConfig.AddComponentTo(this.gameObject);
			_powerWeaponBehaviour.Setup(_powerWeaponConfig);

			StartCoroutine(IncreaseAmmo());
		}

		private IEnumerator IncreaseAmmo()
		{
			while (true)
			{
				yield return new WaitForSeconds(_ammoAutoIncreasePerSecond);
				
				_primaryWeaponBehaviour.IncreaseAmmo(1);
			}
		}

		public void InitializeWeaponSystem()
		{
			SetupPrimaryWeapon();

            if (GunOnStart())
                PutGunInHand();
		}

		public bool LowOnAmmo()
		{
			return _weaponSystemLogic.LowOnAmmo(_primaryWeaponBehaviour.remainingAmmo, _lowAmmoThreshold);
		}

		public void UsePrimaryWeapon(Vector3 direction)
        {
			_primaryWeaponBehaviour.Fire(direction);
        }

		public void AttemptSpecialAbility()
        {
            if (!EnergyLevelExists())
                return;

            UseSpecialAbility();
        }

        private void UseSpecialAbility()
        {	
            _energySystem.ConsumeEnergy(_specialAbilty.energyToConsume);
			var socketObject = _specialAbilty.SetupSocket();
			var behaviour = _specialAbilty.AddComponentTo(socketObject);
			behaviour.SetupConfig(_specialAbilty, GetComponent<Character>());
            _specialAbilty.Use(behaviour);
        }

        private void SetupPrimaryWeapon()
        {
			if (GetComponent<WeaponBehaviour>()) 
				Destroy(GetComponent<WeaponBehaviour>());
	
            _primaryWeaponBehaviour = this.gameObject.AddComponent<WeaponBehaviour>();
            _primaryWeaponBehaviour.Setup(_primaryWeapon as WeaponConfig);
        }

		private bool EnergyLevelExists()
		{
			var energySystem = GetComponent<EnergySystem>();
			Assert.IsNotNull(energySystem);
			return energySystem.HasEnergy(_specialAbilty.energyToConsume) ? true : false;
		}

		private void FindWeaponGripTranform()
        {
            _weaponGrip = GetComponentsInChildren<WeaponGrip>();

			//Assertion ensures that only 1 weapon grip transform is n the child of the character. 
            if (_weaponGrip.Length > 1) 
				Debug.LogError("There should only be one weapon Grip transform in the child.");
			
            if (_weaponGrip.Length == 0) 
				Debug.LogError("You need to add a weaponGrip Transform as a child of " + name);
        }

		public bool ShotIsFired(bool _isBot, ControllerBehaviour _controller)
		{
			Assert.IsTrue(GetComponent<Character>().characterCanShoot, "The Character should be able to shoot if this function is called. ");
			
			var projectileDirection = GetProjectileDirection(_isBot, _controller);

			if (projectileDirection.magnitude < rightAnalogStickThreshold)
				return false;

			UsePrimaryWeapon(projectileDirection);

			return true;
		}

        public void AttemptWeaponCharge()
        {
			if(_energySystem.energyAsPercentage <= _thresholdToAllowChargeOfPowerWeawpon)
			{
				ChargePowerWeapon();
			} 
			else 
			{
				print("Need more time");
			}
            
        }

        public void ChargePowerWeapon()
		{
			_powerWeaponBehaviour.StartCharging();
		}

        public void UsePowerWeapon()
        {
            _powerWeaponBehaviour.Use();
        }

        private bool GunOnStart()
		{
			return _primaryWeapon != null;
		}

		private void PutGunInHand()
		{
			FindWeaponGripTranform();

			//Detect whether weapon is equipped. 
			var weapon = _primaryWeapon;
			var weaponPrefab = weapon.GetItemPrefab();
			var weaponObject = Instantiate(weaponPrefab) as GameObject;

			//Set the weapon Grip Transform.
			weaponObject.transform.SetParent(_weaponGrip[0].transform);
			weaponObject.transform.localPosition = (weapon as IWeaponGrip).weaponGripTransform.position;
			weaponObject.transform.localRotation = (weapon as IWeaponGrip).weaponGripTransform.rotation;
		}

		public Vector3 GetProjectileDirection(bool isBot, ControllerBehaviour controller)
        {
				

			float shouldBeZero = 0; //Keeps the projectile shooting at horizontal plane from start.
			var projectileDirection = Vector3.zero;

			if (!isBot)
			{
				projectileDirection = new Vector3(
					controller.GetRightStickHorizontal(),
					shouldBeZero,
					controller.GetRightStickVertical()
				); //DO NOT NORMALIZES THIS.  MESSES UP THE SYSTEM.

			} 
			return projectileDirection;
        }
	}
}

