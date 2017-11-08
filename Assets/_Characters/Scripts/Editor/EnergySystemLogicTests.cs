using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using NUnit.Framework;

namespace Game.Characters.UnitTests{
	[TestFixture]
	public class EnergySystemLogicTests {
		[Test]
		[TestCase(100, 50, true)]
		[TestCase(50, 50, true)]
		[TestCase(25, 50, false)]
		[TestCase(0, 50, false)]
		public void EnergySystemLogic_HasEnergy_ReturnsIfEnoughEnergy(float currentEnergyLevel, float energyToConsume, bool response)
		{
			var sut = new EnergySystemLogic();
			Assert.AreEqual(response, sut.HasEnergy(currentEnergyLevel, energyToConsume));
		}
	}

}
