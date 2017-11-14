using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons{

    [CreateAssetMenu(menuName = "Game/Throwable Weapon")]
    public class ThrowableWeaponConfig : WeaponConfig
    {	

			[Space]
			[Header("Throwable Weapon Specific")]
			[Tooltip("This adjusts the amount of time the weapon will explode after teh first collision with any object.")]
			[SerializeField] float _blastDelayAfterCollision = 2f;
			public float blastDelayAfterCollision{get{return _blastDelayAfterCollision;}}	
			[SerializeField] BlastConfig _blastConfig;
			public BlastConfig blastConfig{get{return _blastConfig;}}
			public override BlastConfig GetBlastConfig()
			{
				return _blastConfig;
			}
			public override float GetBlastDelayAfterCollision()
			{
				return _blastConfig.delayBeforeExplosion;
			}
    }
}