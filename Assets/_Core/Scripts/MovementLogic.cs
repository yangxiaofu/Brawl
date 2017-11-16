using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class MovementLogic{
		public bool CanJumpOrDash(bool frozen, bool hasEnergy)
		{
			return !frozen && hasEnergy;
		}
	}
}

