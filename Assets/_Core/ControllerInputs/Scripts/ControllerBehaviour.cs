using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Core.ControllerInputs{
	public abstract class ControllerBehaviour : MonoBehaviour {	
		public const string HORIZONTAL = "Horizontal";
		public const string VERTICAL = "Vertical";
		protected Vector3 _inputs = Vector3.zero;
		public Vector3 inputs{
			get{return _inputs;}
		}

		public abstract float GetRightStickVertical();
		public abstract float GetRightStickHorizontal();
		public abstract float GetDigitalPadVertical();
		public abstract float GetDigitalPadHorizontal();
		public abstract void RegisterToController(Player control);

	}

}
