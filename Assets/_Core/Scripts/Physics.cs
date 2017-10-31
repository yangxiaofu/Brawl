using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class GamePhysics {
		private readonly Vector3 _dir;
		private readonly float _speed;
		public GamePhysics(Vector3 direction, float speed){
			_dir = direction;
			_speed = speed;
		}
		public Vector3 GetForce()
		{
			return _dir * _speed;
		}
	}
}

