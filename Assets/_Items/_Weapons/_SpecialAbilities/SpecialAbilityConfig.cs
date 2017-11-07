using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	
	public abstract class SpecialAbilityConfig : ScriptableObject {
		[Header("Special Ability General")]
		[SerializeField] float _energyToConsume = 50f;
		public float energyToConsume{get{return _energyToConsume;}}
		public abstract void Use();
	}
}


