using NUnit.Framework;
using UnityEngine;

namespace Game.Characters.UnitTests
{
    [Category("Character Danger Logic")]
	[TestFixture]
	public class CharacterDangerLogicTests 
	{
		[Test]
		[TestCase(0.8f, 0.2f, false)]
		[TestCase(0.3f, 0.5f, true)]
		[TestCase(0.2f, 0.2f, true)]
		public void CharacterDangerLogic_IsInDanger_ReturnsIfCharacterInDanger(float healthAsPercentage, float inDangerThreshold, bool response)
		{
			var sut = new CharacterDangerLogic(healthAsPercentage, inDangerThreshold);
			Assert.AreEqual(response, sut.IsInDanger());
		}
		
	}
}


