using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.ControllerInputs{
	public abstract class ControllerBehaviour : MonoBehaviour {	
		protected Vector3 _inputs = Vector3.zero;
		public Vector3 inputs{
			get{return _inputs;}
		}

		public abstract void RegisterToController(CharacterControl control);
	}

}
