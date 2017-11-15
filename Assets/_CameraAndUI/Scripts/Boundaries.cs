using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CameraUI{
	[System.Serializable]
	public class Boundaries
	{
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
