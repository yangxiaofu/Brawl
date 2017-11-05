using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{

    [CreateAssetMenu(menuName = "Game/Throwable Weapon")]
    public class ThrowableWeaponConfig : WeaponConfig
    {	
		[Tooltip("This adjusts the amount of time the weapon will explode after teh first collision with any object.")]
		[SerializeField] float _blastDelayAfterCollision = 2f;
		public float blastDelayAfterCollision{get{return _blastDelayAfterCollision;}}
		[SerializeField] float _blastRadius = 2f;
		public float blastRadius{get{return _blastRadius;}}
        public override void Use() //TODO: Potentially remove. 
        {
            //Be thrown from the character position. 
			
			//Explode from landing position. 
        }
    }
}

