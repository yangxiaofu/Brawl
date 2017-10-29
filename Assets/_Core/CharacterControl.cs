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
		[SerializeField] float _jumpHeight = 10f;
		[SerializeField] float _dashDistance = 5f;
		Rigidbody _rb;
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
            {
                Jump();
            }

			if (buttonPressed == PS4_Controller_Input.Button.CIRCLE)
			{
				Dash();
			}
        }

        private void Dash()
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, _dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime)));
			
            _rb.AddForce(dashVelocity, ForceMode.VelocityChange);
        }

        private void Jump()
        {
            if (_isGrounded)
                _rb.AddForce(Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }
}

