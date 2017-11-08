using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
	public class GamePhysics 
	{
		private readonly Vector3 _dir;
		private readonly float _speed;
		private readonly float _jumpFactor = -2f;
		public GamePhysics(Vector3 direction, float speed){
			_dir = direction;
			_speed = speed;
		}

		public GamePhysics(){

		}

		public Vector3 GetForce()
		{
			return _dir * _speed;
		}

		public Vector3 GetAppliedForceWithGravity(Vector3 direction, float jumpHeight)
		{
			return direction * Mathf.Sqrt(
				jumpHeight * 
				_jumpFactor * 
				UnityEngine.Physics.gravity.y
			);
		}
	}
}

