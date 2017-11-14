using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Game.Weapons;

namespace Game.Weapons.UnitTests{
	[TestFixture]
	public class PowerWeaponBehaviourLogicTests{
		[Test]
		[TestCase(false, false, false)]
		[TestCase(true, false, false)]
		[TestCase(false, true, false)]
		[TestCase(true, true, true)]
		public void CanCharge_ReturnsIfCanCharge(bool chargingAllowed, bool hasEnergy, bool result){

			var sut = new PowerWeaponBehaviourLogic();
			Assert.AreEqual(result, sut.CanCharge(chargingAllowed, hasEnergy));
		}
	}

}
