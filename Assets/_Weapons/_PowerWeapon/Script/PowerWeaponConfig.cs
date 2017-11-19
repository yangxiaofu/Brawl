using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons
{
	[CreateAssetMenu(menuName = "Game/Power Weapon")]
    public class PowerWeaponConfig : WeaponConfig
    {		
			[Tooltip("This changes the rate at which the character energy is charged.  The higher the number the faster.")]
			[SerializeField] float _chargePerSecond = 20f;
			public float chargePerSecond{get{return _chargePerSecond;}}

			[Tooltip("The is the rate at which energy is consumed when holding down the power button on the controller or keypad.")]
			[SerializeField] float _energyConsumptionPerSecond = 10f;
			public float energyConsumptionPerSecond{get{return _energyConsumptionPerSecond;}}

			[Tooltip("The force factor is the amount of force that is applied to the projectile once it's launched from the socket it shoots from.")]
			[SerializeField] float _forceFactor = 1000f;
			public float forceFacter{get{return _forceFactor;}}
			
			[Tooltip("The explosion force is the amount of force applyed to damageable game objects once it explodes.")]
			[SerializeField] float _explosionForceFactor = 200f;
			public float explosionForceFactor{get{return _explosionForceFactor;}}

			[Tooltip("This is the rate at which the projectile will increase speed when it's charged.")]
			[SerializeField] float _projectileIncreasePerSecondOnCharge = 0.1f;
			public float projectileIncreasePerSecondOnCharge{get{return _projectileIncreasePerSecondOnCharge;}}

			[Tooltip("IDamageable Objects within the blast radius will see an impact when this triggers")]
			[SerializeField] float _blastRadius = 5f;
			public float GetBlastRadius(){return _blastRadius;}

			public PowerWeaponBehaviour AddComponentTo(GameObject gameObjectToAddTo)
			{
				return gameObjectToAddTo.AddComponent<PowerWeaponBehaviour>();
			}
    }
}

