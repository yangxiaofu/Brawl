using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Panda;

namespace Game.Core{
	public class CameraAI : MonoBehaviour {
		List<GameObject> _characterObjects = new List<GameObject>();
		Vector3 _averagePos;

		CameraAILogic _logic;
		void Start()
        {
            _logic = new CameraAILogic();

            SetupCharacters();

        }

        private void SetupCharacters()
        {
            var character = GameObject.FindObjectsOfType<Character>();

            Assert.AreNotEqual(0, character.Length, "You need to have characters in the game scene");
            for (int i = 0; i < character.Length; i++)
            {
                _characterObjects.Add(character[i].gameObject);
            }
        }

        [Task]
		void FindAveragePositionForCharacters()
		{
			_averagePos = _logic.FindAveragePositionFromGameObjects(_characterObjects);

			Task.current.Succeed();
		}

		[Task]
		void PositionCamera()
		{
			this.transform.position = new Vector3(_averagePos.x, this.transform.position.y, this.transform.position.z);
			Task.current.Succeed();
		}
	}
}

