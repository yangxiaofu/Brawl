using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CameraUI{
	public class CameraBoundariesLogic{
		public bool PositionWithinBoundaries(Vector2 positionOnScreen, Boundaries boundaries)
		{
			return positionOnScreen.x <= boundaries.rightBoundary && positionOnScreen.x >= boundaries.leftBoundary && positionOnScreen.y <= boundaries.topBoundary && positionOnScreen.y >= boundaries.bottomBoundary;
		}
	}
}

