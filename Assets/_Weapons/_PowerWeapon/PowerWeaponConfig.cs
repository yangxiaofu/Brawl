using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons
{
	[CreateAssetMenu(menuName = "Game/Power Weapon")]
    public class PowerWeaponConfig : WeaponConfig
    {
			[SerializeField] float _chargePerSecond = 20f;
			public float chargePerSecond{get{return _chargePerSecond;}}
			[SerializeField] float _forceFactor = 1000f;
			public float forceFacter{get{return _forceFactor;}}
			[SerializeField] BlastConfig _blastConfig;
			public override BlastConfig GetBlastConfig(){ return _blastConfig ;}
			[SerializeField] float _energyConsumptionPerSecond = 10f;
			public float energyConsumptionPerSecond{get{return _energyConsumptionPerSecond;}}
			[SerializeField] float _projectileIncreasePerSecondOnCharge = 0.1f;
			public float projectileIncreasePerSecondOnCharge{get{return _projectileIncreasePerSecondOnCharge;}}

			public PowerWeaponBehaviour AddComponentTo(GameObject gameObjectToAddTo)
			{
				return gameObjectToAddTo.AddComponent<PowerWeaponBehaviour>();
			}

			public override float GetBlastDelayAfterCollision()
			{
					return 0f;
			}
    }
}

