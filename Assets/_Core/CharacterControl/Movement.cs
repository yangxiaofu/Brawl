using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Core{
	public class Movement{
		private readonly Character _character;
		public Movement(Character characterControl)
		{
			_character = characterControl;
		}
		public void Jump()
		{
			var appliedForce = GetAppliedForce();
			_character.rigidBody.AddForce
			(
				appliedForce, ForceMode.VelocityChange
			);
		}

		private Vector3 GetAppliedForce()
		{
			return Vector3.up * Mathf.Sqrt(_character.jumpHeight * -2f * UnityEngine.Physics.gravity.y);
		}

		public void Dash()
		{
			Vector3 dashVelocity = Vector3.Scale(_character.transform.forward, _character.dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _character.rigidBody.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _character.rigidBody.drag + 1)) / -Time.deltaTime)));
            _character.rigidBody.AddForce(dashVelocity, ForceMode.VelocityChange);
		}	
	}

}
