using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using NUnit.Framework;

namespace Game.Core.UnitTests{
	[TestFixture]
	public class SpawnerLogicTests {

		[Test] public void T01_SpawnerLogic_ContainsSpawnedObject_ReturnsHasChild()
		{
			var sut = new SpawnerLogic();
			var gameObject = new GameObject("GameObject");
			Assert.AreEqual(false, sut.ContainsSpawnedObject(gameObject));
		}

		[Test] public void T02_SpawnerLogic_ContainsSpawnedObject_ReturnsNoChild()
		{
			var sut = new SpawnerLogic();
			var myGameObject = new GameObject("GameObject");
			var child = new GameObject("Child");
			child.transform.SetParent(myGameObject.transform);
			Assert.AreEqual(true, sut.ContainsSpawnedObject(myGameObject));
		}
	}
}


