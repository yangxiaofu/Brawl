using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Weapons;

namespace Game.Core
{
	public class GamePhysics 
	{
		private readonly Vector3 _dir;
		private readonly float _speed;
		private readonly float _jumpFactor = -2f;
		private readonly IHittable _bodies;
		public GamePhysics(Vector3 direction, float speed){
			_dir = direction;
			_speed = speed;
		}

		public GamePhysics(IHittable bodies)
		{
			_bodies = bodies;
		}

		public void ApplyForce()
		{
			Assert.IsNotNull(_bodies, "In order to call ApplyForce, you must call new GamePhysics(IBodies) and create a new Hittable_Item");

			_bodies.ApplyForce();
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

