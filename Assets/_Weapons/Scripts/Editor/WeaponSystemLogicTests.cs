using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Weapons.UnitTests{
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

		[Test]
		[TestCase(0, 0, 0, 0)]
		[TestCase(1, 0, 1, 1)]
		[TestCase(1, 1, 2, 2)]
		[TestCase(1, 3, 2, 2)]
		public void IncreaseAmmo_RandomAmmoToIncrease_returnUpdateAmmo(float currentAmmo, float increaseAmount, float maxAmount, float result)
		{
			var sut = new WeaponSystemLogic();
			Assert.AreEqual(result, sut.IncreaseAmmo(currentAmmo, increaseAmount, maxAmount));
		}
	}
}

