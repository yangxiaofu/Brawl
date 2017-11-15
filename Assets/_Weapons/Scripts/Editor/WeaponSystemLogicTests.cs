using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Weapons.UnitTests{
	[TestFixture]
	public class WeaponSystemLogicTests  {
		WeaponSystemLogic sut;

		[SetUp]
		public void Setup()
		{
			sut = new WeaponSystemLogic();
		}

		[Test]
		[TestCase(0, 1, true)]
		[TestCase(1, 1, false)]
		[TestCase(2, 1, false)]
		public void WeaponSystemsLogic_LowOnAmmo_ReturnsLowOnAmmo(float currentAmmo, float lowAmmoThreshold, bool response)
		{
			Assert.AreEqual(response, sut.LowOnAmmo(currentAmmo, lowAmmoThreshold));
		}

		[Test]
		[TestCase(0, 0, 0, 0)]
		[TestCase(1, 0, 1, 1)]
		[TestCase(1, 1, 2, 2)]
		[TestCase(1, 3, 2, 2)]
		public void IncreaseAmmo_RandomAmmoToIncrease_returnUpdateAmmo(float currentAmmo, float increaseAmount, float maxAmount, float result)
		{
			Assert.AreEqual(result, sut.IncreaseAmmo(currentAmmo, increaseAmount, maxAmount));
		}

		[Test]
		[TestCase(0.5f, 0.4f, true, true)]
		[TestCase(0.3f, 0.4f, true, false)]
		[TestCase(0.4f, 0.4f, true, true)]
		[TestCase(0.5f, 0.4f, false, false)]
		[TestCase(0.3f, 0.4f, false, false)]
		[TestCase(0.4f, 0.4f, false, false)]
		public void ChargeAllowed_ReturnsIfChargeIsAllowed(float energyAsPercentage, float threshold, bool canCharge, bool result)
		{
			Assert.AreEqual(result, sut.ChargeAllowed(energyAsPercentage, threshold, canCharge));
		}
	}
}

