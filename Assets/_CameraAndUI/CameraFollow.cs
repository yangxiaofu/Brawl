using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Panda;

namespace Game.CameraUI{
	public class CameraFollow : MonoBehaviour {
		[SerializeField] float _smoothSpeed = 10f;
		List<GameObject> _characterObjects = new List<GameObject>();
		GameObject _averagePositionGameObject;
		public GameObject averagePositionGameObject{get{return _averagePositionGameObject;}}
		
		CameraAILogic _logic;

		void Start()
		{
			_logic = new CameraAILogic();

			SetupCharacters();
		}

		void FixedUpdate()
        {
            FindAveragePositionForCharacters();
            HandleCameraPosition();
			
        }

        private void HandleCameraPosition()
        {
			var avgPos = _averagePositionGameObject.transform.position;
            Vector3 desiredPos = new Vector3(avgPos.x, 0, this.transform.position.z);
            Vector3 smoothPos = Vector3.Lerp(this.transform.position, desiredPos, _smoothSpeed * Time.fixedDeltaTime);
            this.transform.position = smoothPos;
        }

        void FindAveragePositionForCharacters()
		{
			var averagePos = _logic.FindAveragePositionFromGameObjects(_characterObjects);
			_averagePositionGameObject = new GameObject("Average Pos");
			_averagePositionGameObject.transform.position = averagePos;
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

	}
}


