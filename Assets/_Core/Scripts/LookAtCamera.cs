using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class LookAtCamera : MonoBehaviour {

		Camera _cam;

		void Start()
		{
			_cam = Camera.main;
		}

		void Update() 
		{
			this.transform.LookAt(_cam.transform);
		}
	}
}

