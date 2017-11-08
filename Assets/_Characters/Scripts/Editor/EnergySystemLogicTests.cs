using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using NUnit.Framework;

namespace Game.Characters.UnitTests{
	[TestFixture]
	public class EnergySystemLogicTests {
		EnergySystemLogic _sut;

		[SetUp]
		public void Setup()
		{
			float minEnergy = 0;
			float maxEnergy = 100f;
			_sut = new EnergySystemLogic(minEnergy, maxEnergy);
		}

		[Test]
		[TestCase(100, 50, true)]
		[TestCase(50, 50, true)]
		[TestCase(25, 50, false)]
		[TestCase(0, 50, false)]
		public void EnergySystemLogic_HasEnergy_ReturnsIfEnoughEnergy(float currentEnergyLevel, float energyToConsume, bool response)
		{
			
			Assert.AreEqual(response, _sut.HasEnergy(currentEnergyLevel, energyToConsume));
		}

		[Test]
		[TestCase(100, 50, 50)]
		[TestCase(100, 200, 0)]
		[TestCase(0, 100, 0)]
		[TestCase(100, 25, 75)]
		public void EnergySystemLogic_ConsumeEnergy_ReturnsEnergyAfterConsumption(float currentEnergyLevel, float energyToConsume, float response)
		{
			Assert.AreEqual(response, _sut.ConsumeEnergy(currentEnergyLevel, energyToConsume));
		}

		[Test]
		[TestCase(50, 10, 60)]
		[TestCase(50, 100, 100)]
		[TestCase(50, 50, 100)]
		public void EnergySystemLogic_IncreaseEnergy_ReturnsCorrectEnergyTotalAfterIncrease(float currentEnergyLevel, float energyToAdd, float response)
		{
			Assert.AreEqual(response, _sut.IncreaseEnergy(currentEnergyLevel, energyToAdd));
		}
	}

}
