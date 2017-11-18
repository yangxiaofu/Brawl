using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Panda;

namespace Game.CameraUI{
	public class CameraFollow : MonoBehaviour {
		[SerializeField] float _smoothSpeed = 10f;
        [SerializeField] bool _followPlayerOnZ = false;
        Camera _camera;
		List<GameObject> _characterObjects = new List<GameObject>();
		GameObject _averagePositionGameObject;
		public GameObject averagePositionGameObject{get{return _averagePositionGameObject;}}
		CameraAILogic _logic;
        float _rotationSpeed = 10f;

		void Start()
		{
            _camera = GetComponentInChildren<Camera>();

			_logic = new CameraAILogic();
			_averagePositionGameObject = new GameObject("Average Pos");
			SetupCharacters();
		}

		void FixedUpdate()
        {
            FindAveragePositionForCharacters();
            HandleCameraPosition();
            HandleCameraRotation();
        }

        private void HandleCameraPosition()
        {
			var avgPos = _averagePositionGameObject.transform.position;

            var zPos = this.transform.position.z;

            if (_followPlayerOnZ)    
                zPos = _averagePositionGameObject.transform.position.z;

            Vector3 desiredPos = new Vector3(avgPos.x, 0, zPos);
            Vector3 smoothPos = Vector3.Lerp(
                this.transform.position, 
                desiredPos, 
                _smoothSpeed * Time.fixedDeltaTime
            );

            this.transform.position = smoothPos;
        }

        private void HandleCameraRotation()
        {
			Vector3 lookAtGoal = _averagePositionGameObject.transform.position;
			Vector3 direction = lookAtGoal - _camera.transform.position;
            
			_camera.transform.rotation = Quaternion.Slerp(
                _camera.transform.rotation, 
                Quaternion.LookRotation(direction), 
                Time.deltaTime * _rotationSpeed
            );
        }

        void FindAveragePositionForCharacters()
		{
			var averagePos = _logic.FindAveragePositionFromGameObjects(_characterObjects);
			_averagePositionGameObject.transform.position = averagePos;
		}

        private void SetupCharacters()
        {
            var character = GameObject.FindObjectsOfType<MyPlayer>();
            Assert.AreNotEqual(0, character.Length, "You need to have characters in the game scene");

            for (int i = 0; i < character.Length; i++)
            {
                _characterObjects.Add(character[i].gameObject);
            }
        }
	}
}


