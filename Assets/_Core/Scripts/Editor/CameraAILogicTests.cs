using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Game.CameraUI;

namespace Game.CameraUI.UnitTests{
	[TestFixture]
	public class CameraAILogicTests {

		[Test]
		public void FindAveragePositionOfVectors()
		{
			List<GameObject> gameObjects = new List<GameObject>();

			var g1 = new GameObject("G1");
			g1.transform.position = new Vector3(1, 1 ,1);
			gameObjects.Add(g1);

			var g2 = new GameObject("G2");
			g2.transform.position = new Vector3(2, 2, 2);
			gameObjects.Add(g2);

			var sut = new CameraAILogic();
			var result = new Vector3(1.5f, 1.5f, 1.5f);
			Assert.AreEqual(result, sut.FindAveragePositionFromGameObjects(gameObjects));
		}

		[Test]
		public void FindAveragePositionOfVectors_Returns2()
		{
			List<GameObject> gameObjects = new List<GameObject>();

			var g1 = new GameObject("G1");
			g1.transform.position = new Vector3(1, 1 ,1);
			gameObjects.Add(g1);

			var g2 = new GameObject("G2");
			g2.transform.position = new Vector3(2, 2, 2);
			gameObjects.Add(g2);

			var g3 = new GameObject("G3");
			g3.transform.position = new Vector3(3, 3, 3);
			gameObjects.Add(g3);

			var sut = new CameraAILogic();
			var result = new Vector3(2f, 2f, 2f);
			Assert.AreEqual(result, sut.FindAveragePositionFromGameObjects(gameObjects));

			
		}

		[Test]
		public void FindAveragePositionOfVectors_Returns2_5()
		{
			List<GameObject> gameObjects = new List<GameObject>();

			var g1 = new GameObject("G1");
			g1.transform.position = new Vector3(1, 1 ,1);
			gameObjects.Add(g1);

			var g2 = new GameObject("G2");
			g2.transform.position = new Vector3(2, 2, 2);
			gameObjects.Add(g2);

			var g3 = new GameObject("G3");
			g3.transform.position = new Vector3(3, 3, 3);
			gameObjects.Add(g3);

			var g4 = new GameObject("G3");
			g4.transform.position = new Vector3(4, 4, 4);
			gameObjects.Add(g4);

			var sut = new CameraAILogic();
			var result = new Vector3(2.5f, 2.5f, 2.5f);
			Assert.AreEqual(result, sut.FindAveragePositionFromGameObjects(gameObjects));

			
		}
	}
}

