using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Weapons{
	public class FallingItemBehaviour: MonoBehaviour{
        FallingItemsSpecialAbilityConfig _config;
		public void SetupConfig(FallingItemsSpecialAbilityConfig config, Character character)
        {
            _config = config;
        }
    }
}

