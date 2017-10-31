using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;
using Panda;

namespace Game.Items{
	public class WeaponSystem : MonoBehaviour {
		[SerializeField] WeaponConfig _primaryWeapon;
		WeaponBehaviour _primaryWeaponBehaviour;
		public WeaponBehaviour primaryWeaponBehaviour{get{return _primaryWeaponBehaviour;}}
		public WeaponConfig primaryWeapon{get{return _primaryWeapon;}}
		void Start()
        {
            SetupPrimaryWeapon();
        }

        private void SetupPrimaryWeapon()
        {
			if (GetComponent<WeaponBehaviour>()) 
				Destroy(GetComponent<WeaponBehaviour>());
	
            _primaryWeaponBehaviour = this.gameObject.AddComponent<WeaponBehaviour>();
            _primaryWeaponBehaviour.Setup(_primaryWeapon);
        }

        public void UpdateWeapon(WeaponConfig weaponConfig)
		{
			_primaryWeapon = weaponConfig;
			SetupPrimaryWeapon();
		}
		
		[Task]
		public void ShootProjectile(Vector3 direction, Vector3 projectileSocketPosition)
        {
			_primaryWeaponBehaviour.Fire(direction, projectileSocketPosition);
        }
	}
}

