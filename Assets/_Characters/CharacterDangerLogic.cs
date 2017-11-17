using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class CharacterDangerLogic {
		private readonly float _healthAsPercentage;
		private readonly float _inDangerThreshold;
		public CharacterDangerLogic(float healthAsPercentage, float inDangerThreshold)
		{
			_healthAsPercentage = healthAsPercentage;
			_inDangerThreshold = inDangerThreshold;
		}

		public bool IsInDanger()
		{	
			return _healthAsPercentage <= _inDangerThreshold;
		}
	}
}

