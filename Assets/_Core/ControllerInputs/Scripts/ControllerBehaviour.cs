using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Core.ControllerInputs{
	public abstract class ControllerBehaviour : MonoBehaviour {	
		[SerializeField] protected string _prefix;
		[SerializeField] protected Player _player;
		

		void Awake()
		{
			_player.Setup(this);
		}

		protected Vector3 _inputs = Vector3.zero;
		public Vector3 inputs{
			get{return _inputs;}
		}

		public abstract float GetLeftStickVertical();
		public abstract float GetLeftStickHorizontal();
		public abstract float GetRightStickVertical();
		public abstract float GetRightStickHorizontal();
		public abstract float GetDigitalPadVertical();
		public abstract float GetDigitalPadHorizontal();

	}

}
