using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Core.UnitTests{

	[TestFixture]
	public class TotalPointsCalculatorTests {

		[Test]
		public void T01_TotalPoints_ReturnsZero()
		{
			var totalHits = 0;
			var pointsPerHit = 10f;
			var response = 0;

			var sut = new TotalPointsCalculator(totalHits, pointsPerHit);

			Assert.AreEqual(response, sut.Calculate());
		}

		[Test]
		public void T02_TotalPoints_ReturnsTwenty()
		{
			var totalHits = 2;
			var pointsPerHit = 10f;
			var response = 20f;
			var sut = new TotalPointsCalculator(totalHits, pointsPerHit);

			Assert.AreEqual(response, sut.Calculate());
		}

		[Test]
		public void T03_TotalPoints_ReturnsFifty()
		{
			var totalHits = 2;
			var pointsPerHit = 25;
			var response = 50f;
			var sut = new TotalPointsCalculator(totalHits, pointsPerHit);

			Assert.AreEqual(response, sut.Calculate());
		}
	}
}

