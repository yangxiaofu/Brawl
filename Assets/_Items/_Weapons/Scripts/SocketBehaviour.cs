using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items{
	public class SocketBehaviour : MonoBehaviour {
		protected Character _character;
		protected SpecialAbilityConfig _config;
		public void SetupConfig(SpecialAbilityConfig config, Character character)
		{
			_config = config;
            _character = character; 
		}
	}
}

