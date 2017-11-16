using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Core.UnitTests{
	[TestFixture]
	public class MovementLogicTests 
	{
		[Test]
		[TestCase(false, true, true)]
		[TestCase(true, true, false)]
		[TestCase(true, false, false)]
		[TestCase(false, false, false)]
		public void CanJumpOrDash_ReturnsIfCanJump(bool frozen, bool hasEnergy, bool result)
		{
			var sut = new MovementLogic();
			Assert.AreEqual(result, sut.CanJumpOrDash(frozen, hasEnergy));
		}
	}
}

