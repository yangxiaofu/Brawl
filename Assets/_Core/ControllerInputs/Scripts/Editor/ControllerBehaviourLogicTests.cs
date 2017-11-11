using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Game.Core;

namespace Game.Core.UnitTests
{

	[TestFixture]
	public class ControllerBehaviourLogicTests 
	{
		[Test]
		[TestCase(10f, 10f, 10f, 1f)]
		[TestCase(1f, 0f, 1f, 1f)]
		[TestCase(0.5f, 0f, 0.71f, 1f)]
		[TestCase(3.0f, 0f, 2.0f, 1f)]
		[TestCase(0.1f, 0f, 0f, 1f)]
		public void GetMovementInputs_ReturnsMagnitudeLessThanOne(float x, float y, float z, float result)
		{
			Vector3 v = new Vector3(x, y, z);
			var sut = new ControllerBehaviourLogic();
			Assert.That(result, Is.InRange(0.98, 1.01));
		}
		
	}

}
