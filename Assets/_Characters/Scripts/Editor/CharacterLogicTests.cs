using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Weapons;
using NUnit.Framework;

namespace Game.Characters.UnitTests{

	[TestFixture]
	public class CharacterLogicTests {
		Character _character;
		CharacterLogic _sut;

		[SetUp]
		public void Setup()
		{
			var characterObject = new GameObject("Character");
			_character = characterObject.AddComponent<Character>();
			_sut = new CharacterLogic(_character);
		}

		[Test]
		public void CharacterLogic_IsCollisionWithSelf_ReturnsFalse()
		{
			Assert.AreEqual(false, _sut.WillTakeDamageFrom(_character.gameObject));
		}

		[Test]
		public void CharacterLogic_IsCollisionWithOtherCharacter_ReturnsFalse(){
			var otherCharacter = new GameObject("OtherCharacter");
			Assert.AreEqual(false, _sut.WillTakeDamageFrom(otherCharacter));
		}

		[Test]
		public void CharacterLogic_IsCollisionWithProjectile_ReturnsTrue()
		{		
			var projectile = new GameObject("TestProjectile");
			projectile.AddComponent<ProjectileBehaviour>();
			Assert.AreEqual(true, _sut.WillTakeDamageFrom(projectile));
		}

		[Test]
		[TestCase(true, true, false)]
		[TestCase(false, false, false)]
		[TestCase(true, false, false)]
		[TestCase(false, true, true)]
		public void CanMove_ReturnsIfCharacterCanMove(bool frozen, bool hasController, bool result){
			Assert.AreEqual(result, _sut.CanMove(frozen, hasController));
		}
	}
}

