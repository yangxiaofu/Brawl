using System;   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Core.ControllerInputs{
	public abstract class ControllerBehaviour : MonoBehaviour {	
		[SerializeField] protected Character _character;
        [SerializeField] protected CharacterMovement _movement;
		public Character character{ get{return _character;}}
		[SerializeField] bool _isBOT = false;
		protected string _prefix;
		[SerializeField] PLAYER_TAG _playerTag;
        protected Vector3 _inputs = Vector3.zero;
        public ControllerBehaviourLogic _logic;
        public Vector3 GetMovementInputs()
        {
            return _inputs;
        }

		public PLAYER_TAG playerTag{get{return _playerTag;}}
		void Awake()
		{
            _logic = new ControllerBehaviourLogic();

            if (_character){
                _character.Setup(this);		
			    _character.isBot = _isBOT;	
            } else if (_movement){
                _movement.Setup(this);
            }
			
		}

        protected void InitializeControllerPrefix()
        {
            if (_playerTag == PLAYER_TAG.PLAYER_1)
            {
                _prefix = "P1";
            }
            else if (_playerTag == PLAYER_TAG.PLAYER_2)
            {
                _prefix = "P2";
            }
            else if (_playerTag == PLAYER_TAG.PLAYER_3)
            {
                _prefix = "P3";
            }
            else if (_playerTag == PLAYER_TAG.PLAYER_4)
            {
                _prefix = "P4";
            }
            else
            {
                Debug.LogError("Should never go here.");
            }
        }

		public abstract float GetLeftStickVertical();
		public abstract float GetLeftStickHorizontal();
		public abstract float GetRightStickVertical();
		public abstract float GetRightStickHorizontal();
		public abstract float GetDigitalPadVertical();
		public abstract float GetDigitalPadHorizontal();
    }

}
