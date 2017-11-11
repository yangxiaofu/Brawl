﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Core.UnitTests{
	[TestFixture]
	public class GamePhysicsTests 
	{
		
		[Test]
		public void T01_GetAppliedForce_ReturnsUp()
		{
			var sut = new GamePhysics();
			var jumpHeight = 2.0f;
			
			Assert.AreEqual(14.1421356f, sut.GetAppliedForceWithGravity(Vector3.up, jumpHeight).magnitude);
		}
	}
}

