using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Characters.UnitTests
{
	[TestFixture]
	public class HealthSystemLogicTests 
	{
		HealthSystemLogic sut;

		[SetUp]
		public void Setup(){
			var minHealth = 0;
			var maxHealth = 100f;
			sut = new HealthSystemLogic(minHealth, maxHealth);
		}

		[Test]
		[TestCase(50, 50, 100, 100)]
		[TestCase(50, 100, 100, 100)]
		[TestCase(50, 25, 100, 75)]
		[TestCase(50, 0, 100, 50)]
		public void HealthSystemLogic_AddHealth_ReturnsIncreasedHealth(float currentHealth, float healthToAdd, float maxHealth, float response)
		{
			Assert.AreEqual(response, sut.IncreaseHealth(currentHealth, healthToAdd));
		}

		[Test]
		[TestCase(50, 25, 100, 25)]
		[TestCase(50, 75, 100, 0)]
		public void HealthSystemLogic_AddHealth_ReturnsReducedHealth(float currentHealth, float damageToTake, float maxHealth, float response){
			Assert.AreEqual(response, sut.TakeDamage(currentHealth, damageToTake));
		}
	}
}

