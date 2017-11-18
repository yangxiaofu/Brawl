using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Weapons;
using NUnit.Framework;

namespace Game.Weapons.UnitTests
{
	[TestFixture]
	public class WeaponBehaviourTests  
	{
		GameObject _weaponBehaviourObject;
		RangeWeaponBehaviour _sut;

		[SetUp]
		public void Setup()
		{
			_weaponBehaviourObject = new GameObject("WeaponBehaviourTests");
			_sut = _weaponBehaviourObject.AddComponent<RangeWeaponBehaviour>();
		}

		[Test]
		public void T01_WeaponBehaviour_Fire_ReturnsOneless()
		{
			int remainingAmmo = 10;
			int startingAmmo = 10;
			int result = 9;

			_sut.Setup(remainingAmmo, startingAmmo);

			Vector3 directionFake = Vector3.zero;
			_sut.Fire(directionFake);

			Assert.AreEqual(result, _sut.remainingAmmo);
		}

		[Test]
		[TestCase(10, 10, 1, 9)]
		[TestCase(10, 10, 10, 0)]
		[TestCase(10, 10, 15, 0)]
		[TestCase(10, 10, 0, 10)]
		public void T02_WeaponBehaviour_ReduceAmmoBy_ReturnsAmmoAfterReduction(int remainingAmmo, int startingAmmo, int amountToReduceAmmoBy, int result)
		{			
			_sut.Setup(remainingAmmo, startingAmmo);
			_sut.ReduceAmmoBy(amountToReduceAmmoBy);
			Assert.AreEqual(result, _sut.remainingAmmo);
		}

		[Test]
		[TestCase(10, 10, 1, 10)]
		[TestCase(5, 10, 1, 6)]
		[TestCase(5, 10, 6, 10)]
		[TestCase(5, 10, 0, 5)]
		public void T03_WeaponBehaviour_IncreaseAmmo_ReturnsAmmoAfterIncrease(int remainingAmmo, int startingAmmo, int amountToIncreaseAmmoBy, int result)
		{			
			_sut.Setup(remainingAmmo, startingAmmo);
			_sut.IncreaseAmmo(amountToIncreaseAmmoBy);
			Assert.AreEqual(result, _sut.remainingAmmo);
		}
	}

}
