using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Panda;
using System;

namespace Game.CameraUI{
	public class CameraAI : MonoBehaviour {
		CameraFollow _cameraFollow;
		float _rotationSpeed = 10f;

		void Start()
		{
			_cameraFollow = GetComponentInParent<CameraFollow>();
		}


		void FixedUpdate()
		{
			HandleCameraRotation();
		}

        private void HandleCameraRotation()
        {
			Vector3 lookAtGoal = _cameraFollow.averagePositionGameObject.transform.position;
			Vector3 direction = lookAtGoal - this.transform.position;
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _rotationSpeed);
        }
    }
}

