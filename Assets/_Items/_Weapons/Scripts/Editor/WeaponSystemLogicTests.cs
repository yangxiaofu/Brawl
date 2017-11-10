using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Items.UnitTests{
	[TestFixture]
	public class WeaponSystemLogicTests  {
		[Test]
		[TestCase(0, 1, true)]
		[TestCase(1, 1, false)]
		[TestCase(2, 1, false)]
		public void WeaponSystemsLogic_LowOnAmmo_ReturnsLowOnAmmo(float currentAmmo, float lowAmmoThreshold, bool response)
		{
			var sut = new WeaponSystemLogic();
			Assert.AreEqual(response, sut.LowOnAmmo(currentAmmo, lowAmmoThreshold));
		}
	}
}

