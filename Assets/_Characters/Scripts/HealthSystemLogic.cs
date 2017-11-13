using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class HealthSystemLogic {

		public Vector3 GrowOpponent(Vector3 currentScale, int timesHit, float scaleFactor)
		{
			var factor = 1 + (timesHit * scaleFactor);
			return currentScale * factor;
		}

	}

}
