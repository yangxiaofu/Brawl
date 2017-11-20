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
		[Header("Weapons and Special Abilities")]
		[Tooltip("The primary weapon is the weapon that shoots from the right trigger of your controller.  All players will have a primary weapon.")]
		[SerializeField] WeaponConfig _primaryWeapon;
		public WeaponConfig primaryWeapon{get{return _primaryWeapon;}}
		[SerializeField] PowerWeaponConfig _powerWeapon;

		[Space]
		[Header("Configurations")]

		[Tooltip("This is the minimum percentage of maximum energy that allows you to charge your power weapon.")]
		[Range(0, 1)][SerializeField] float _thresholdToAllowChargeOfPowerWeawpon = 0.4f;

		[Tooltip("This is the rate at which ammo is regenerated for your primary weapon.")]
		[SerializeField] float _ammoAutoIncreasePerSecond = 1;

		[Tooltip("Toggling this allows you to adjust the amount of allowable time you can charge your power weapon after it's last use.")]
		[SerializeField] float _timeBetweenPowerWeaponUse = 1f;
		[Space]
		[Header("Hit Attributes")]
		[Range(0, 1)]
		[SerializeField] float _probabilityOfSuccessfulHit = 0.5f;
		[SerializeField] float _hitRadius = 0.3f;
		[SerializeField] float _hitDamage = 5f;
		float rightAnalogStickThreshold = 0.9f;
		RangeWeaponBehaviour _primaryWeaponBehaviour;
		public RangeWeaponBehaviour primaryWeaponBehaviour{get{return _primaryWeaponBehaviour;}}
		WeaponGrip[] _weaponGrip;
		EnergySystem _energySystem;
		WeaponSystemLogic _weaponSystemLogic;
		PowerWeaponBehaviour _powerWeaponBehaviour;
		bool _canCharge = true;
		void Start()
		{	
			_energySystem = GetComponent<EnergySystem>();
			Assert.IsNotNull(_energySystem);

			_weaponSystemLogic = new WeaponSystemLogic();

			_powerWeaponBehaviour = _powerWeapon.AddComponentTo(this.gameObject);
			_powerWeaponBehaviour.Setup(_powerWeapon);

			StartCoroutine(IncreaseAmmo());
		}

		void Hit() //TODO: Create Enemy Weapon System to differentiate this from character weapon system later. 
		{ // CALLBACK FROM THE ANIMATOR.
			var r = UnityEngine.Random.Range(0f, 1f);

			if (r < _probabilityOfSuccessfulHit)
			{
				GetComponent<EnemyAI>().target.GetComponent<HealthSystem>().DealDamage(_hitDamage);	
				//TODO: Play Hit Sound
				//TODO: Do some particle prefab to show a hit. 
			} else {
				//TODO: Player Missed hit sound.
				//TODO: Do some particle prefab to show am issed hit. 
			}
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

		public void UsePrimaryWeapon(Vector3 direction)
        {
			_primaryWeaponBehaviour.Fire(direction);
        }

        private void SetupPrimaryWeapon()
        {
			if (GetComponent<RangeWeaponBehaviour>()) 
				Destroy(GetComponent<RangeWeaponBehaviour>());
	
            _primaryWeaponBehaviour = this.gameObject.AddComponent<RangeWeaponBehaviour>();
            _primaryWeaponBehaviour.Setup(_primaryWeapon as WeaponConfig);
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

		public bool ShotIsFired(ControllerBehaviour _controller)
		{
			Assert.IsTrue(
				GetComponent<Character>().characterCanShoot, 
				"The Character should be able to shoot if this function is called. "
			);
			
			var projectileDirection = GetProjectileDirection(_controller);
			if (projectileDirection.magnitude < rightAnalogStickThreshold)
				return false;

			UsePrimaryWeapon(projectileDirection);

			return true;
		}

        public void AttemptPowerWeaponCharge()
        {
			if(_weaponSystemLogic.ChargeAllowed(_energySystem.energyAsPercentage, _thresholdToAllowChargeOfPowerWeawpon, _canCharge))
				ChargePowerWeapon();
        }

        public void ChargePowerWeapon()
		{
			_powerWeaponBehaviour.StartCharging();
			_canCharge = false;
		}

        public void UsePowerWeapon()
        {
            _powerWeaponBehaviour.Use();
			StartCoroutine(UpdateCanCharge(_timeBetweenPowerWeaponUse));
        }

		IEnumerator UpdateCanCharge(float delay)
		{
			yield return new WaitForSeconds(delay);
			_canCharge = true;
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

		private Vector3 GetProjectileDirection(ControllerBehaviour controller)
        {
			float shouldBeZero = 0; //Keeps the projectile shooting at horizontal plane from start.
			var projectileDirection = Vector3.zero;

			projectileDirection = new Vector3(
				controller.GetRightStickHorizontal(),
				shouldBeZero,
				controller.GetRightStickVertical()
			); //DO NOT NORMALIZES THIS.  MESSES UP THE SYSTEM.

			return projectileDirection;
        }
	}
}

