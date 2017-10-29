using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class Movement{

		private readonly CharacterControl _character;

		public Movement(CharacterControl characterControl)
		{
			_character = characterControl;
		}
		public void Jump()
		{
			_character.rigidBody.AddForce(Vector3.up * Mathf.Sqrt(_character.jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
		}

		public void Dash()
		{
			Vector3 dashVelocity = Vector3.Scale(_character.transform.forward, _character.dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _character.rigidBody.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _character.rigidBody.drag + 1)) / -Time.deltaTime)));
            _character.rigidBody.AddForce(dashVelocity, ForceMode.VelocityChange);
		}	
	}

}
