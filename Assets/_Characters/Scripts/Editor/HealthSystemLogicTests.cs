using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Characters.UnitTests
{
	[TestFixture]
	public class HealthSystemLogicTests 
	{
		[Test]
		[TestCase(1, 0.1f, 1.1f)]
		[TestCase(2, 0.1f, 1.2f)]
		[TestCase(3, 0.1f, 1.3f)]
		public void GrowOpponent_ReturnsBigger(int timesHit, float growthScale, float scaleInAllDirections)
		{	
			var sut = new HealthSystemLogic();
			var originalVector = new Vector3(1, 1, 1);
			var growthVector = sut.GrowOpponent(originalVector, timesHit, growthScale);
			var result = new Vector3(scaleInAllDirections, scaleInAllDirections, scaleInAllDirections);
			Assert.AreEqual(result, growthVector);
		}
	}
}

