using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Game.Weapons;

namespace Game.Weapons.UnitTests{
	[TestFixture]
	public class BlastBehaviourLogicTests{
		[Test]
		[TestCase(true, true, true, true, true)]
		[TestCase(false, true, true, true, false)]
		[TestCase(false, true, true, true, false)]
		[TestCase(true, false, false, true, false)]
		[TestCase(true, true, false, true, false)]
		[TestCase(true, true, true, false, false)]
		[TestCase(false, false, false, false, false)]
		public void CanAddForce_ReturnsForceCanBeAdded(bool isEnemy, bool hasRigidBody, bool hasNavMeshAgent, bool isDead, bool result){
			var sut = new BlastBehaviourLogic();
			Assert.AreEqual(result, sut.CanAddForce(isEnemy, hasRigidBody, hasNavMeshAgent, isDead));
		}
	}
}

