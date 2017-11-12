using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class KickableLogic {
		public bool ForceCanBeAdded(bool inMotion, bool moveable)
		{
			return (inMotion && moveable);
		}

		public bool AnimationCanUpdate(bool frozen, bool hasController)
		{
			if (frozen && !hasController)
			{
				return true;
			} else {
				return false;
			}
		}
	}
}

