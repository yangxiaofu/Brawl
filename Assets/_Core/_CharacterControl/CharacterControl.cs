using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core.ControllerInputs;
using System;

namespace Game.Core{
	public class CharacterControl : MonoBehaviour {
		[Header("Game Controller Reference")]
		[SerializeField] ControllerBehaviour _gameController;

		[Header ("Character Movement Parameters")]
		[SerializeField] float _speed = 5f;
		[SerializeField] float _groundDistance = 0.2f;
		[SerializeField] LayerMask _ground;
		[SerializeField] float _jumpHeight = 5f;
		public float jumpHeight{get{return _jumpHeight;}}
		[SerializeField] float _dashDistance = 0.5f;
		public float dashDistance{get{return _dashDistance;}}
		Movement _movement;
		Rigidbody _rb;
		public Rigidbody rb{get{return _rb;}}
		Transform _groundChecker;
        bool _isGrounded = true;

		void Awake(){

			Assert.IsNotNull(
				_gameController, 
				"A game controller needs to be referenced in the character control."
			);

		}
		void Start()
		{
			_rb = GetComponent<Rigidbody>();
			_groundChecker = transform.GetChild(0);
			_gameController.RegisterToController(this);
			_movement = new Movement(this);
		}

		void Update()
		{
			_isGrounded = Physics.CheckSphere(_groundChecker.position, _groundDistance, _ground, QueryTriggerInteraction.Ignore);

			if (_gameController.inputs != Vector3.zero)	
				transform.forward = _gameController.inputs;
			
		}
		void FixedUpdate()
		{
			_rb.MovePosition(_rb.position + _gameController.inputs * _speed * Time.fixedDeltaTime);
		}

        public void OnButtonPressed(PS4_Controller_Input.Button buttonPressed)
        {
			if (buttonPressed == PS4_Controller_Input.Button.X)
            	if (_isGrounded) _movement.Jump();
            

			if (buttonPressed == PS4_Controller_Input.Button.CIRCLE)
				_movement.Dash();
			
        }
    }
}

