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

	[System.Serializable]
	public class Boundaries{
		public float topBoundary;
		public float bottomBoundary;
		public float leftBoundary;
		public float rightBoundary;
		public Boundaries(float border, float screenWidth, float screenHeight)
		{
			this.topBoundary = screenHeight - border;
			this.bottomBoundary = border;
			this.rightBoundary = screenWidth - border;
			this.leftBoundary = border;
		}
		
	}
}

