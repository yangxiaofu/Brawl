using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class ControllerBehaviourLogic 
	{	
		public Vector3 GetMovementInputs(Vector3 inputs)
		{
			var v = inputs.normalized;
			return v;
		}
	}
}

