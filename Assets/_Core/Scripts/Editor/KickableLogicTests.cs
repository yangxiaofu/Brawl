using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Game.Core;

namespace Game.Core.UnitTests
{
	[TestFixture]
	public class KickableLogicTests {

		[Test]
		[TestCase(true, true, true)]
		[TestCase(false, false, false)]
		[TestCase(false, true, false)]
		[TestCase(true, false, false)]
		public void ForceCanBeAdded_ReturnsTrue(bool inMotion, bool isMoveable, bool result)
		{
			var sut = new KickableLogic();
			Assert.AreEqual(result, sut.ForceCanBeAdded(inMotion, isMoveable));
		}

		[Test]
		[TestCase(true, false, true)]
		[TestCase(true, true, false)]
		[TestCase(false, false, false)]
		[TestCase(false, true, false)]
		public void AnimationCanUpdate_ReturnsIfAnimationCanUpdate(bool frozen, bool hasController, bool result)
		{
			var sut = new KickableLogic();
			Assert.AreEqual(result, sut.AnimationCanUpdate(frozen, hasController));
		}
	}

}
