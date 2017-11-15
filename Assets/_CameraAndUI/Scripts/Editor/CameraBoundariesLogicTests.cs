using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Game.CameraUI;


namespace Game.CameraUI.UnitTests{
	[TestFixture]
	public class CameraBoundariesLogicTests {
		[Test]
		[TestCase(1, 5, 5, true)]
		[TestCase(1, 2, 8, true)]
		[TestCase(1, 0, 5, false)]
		[TestCase(1, 11, 5, false)]
		[TestCase(1, 5, 11, false)]
		[TestCase(1, 5, 0, false)]
		public void IsWithinOuterBoundary_ReturnsIfWithinBoundary(float border, float xPos, float yPos, bool result)
		{
			var screenWidth = 10f;
			var screenHeight = 10f;
			var boundaries = new Boundaries(border, screenWidth, screenHeight);
			var sut = new CameraBoundariesLogic();
			var positionOnScreen = new Vector2(xPos, yPos);
			Assert.AreEqual(result, sut.PositionWithinBoundaries(positionOnScreen, boundaries));
		}
			
	}
}

