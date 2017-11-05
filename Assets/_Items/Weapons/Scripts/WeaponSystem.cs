using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Core;
using Game.Core.ControllerInputs;
using Panda;

namespace Game.Items{
	
	public class WeaponSystem : MonoBehaviour {
		[SerializeField] WeaponConfig _primaryWeapon;
		public WeaponConfig primaryWeapon{get{return _primaryWeapon;}}
		[SerializeField] WeaponConfig _secondaryWeapon;
		float rightAnalogStickThreshold = 0.9f;
		WeaponBehaviour _primaryWeaponBehaviour;
		public WeaponBehaviour primaryWeaponBehaviour{get{return _primaryWeaponBehaviour;}}
		WeaponGrip[] _weaponGrip;
		public void InitializeWeaponSystem()
		{
			SetupPrimaryWeapon();

            if (GunOnStart())
                PutGunInHand();
		}
        private void SetupPrimaryWeapon()  //TODO: remove to the main character code. 
        {
			if (GetComponent<WeaponBehaviour>()) 
				Destroy(GetComponent<WeaponBehaviour>());
	
            _primaryWeaponBehaviour = this.gameObject.AddComponent<WeaponBehaviour>();
            _primaryWeaponBehaviour.Setup(_primaryWeapon as WeaponConfig);
        }

        public void UpdateWeapon(WeaponConfig weaponConfig)
		{
			_secondaryWeapon = weaponConfig;
		}

		public void UseSecondaryWeapon() //Shoots in the direction of the transform. 
		{
			Debug.Log("Shoot from secondary weapon");
			var projectilePrefab = (_secondaryWeapon as RangeWeaponConfig).projectileConfig.GetProjectilePrefab();
		}
		
		public void UsePrimaryWeapon(Vector3 direction)
        {
			_primaryWeaponBehaviour.Fire(direction);
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
			
			var projectileDirection = GetProjectileDirection(_isBot, _controller);

			if (projectileDirection.magnitude < rightAnalogStickThreshold)
				return false;

			UsePrimaryWeapon(projectileDirection);

			return true;
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
			weaponObject.transform.localPosition = weapon.weaponGripTransform.position;
			weaponObject.transform.localRotation = weapon.weaponGripTransform.rotation;
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

