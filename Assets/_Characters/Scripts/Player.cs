using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core.ControllerInputs;
using Game.Weapons;
using Game.Core;
using System;

namespace Game.Characters{
	public class Player : Character {
		[Header("Game Controller Reference")]
		[SerializeField] ControllerBehaviour _gameController;
		void Start()
        {
            InitializeVariables();

            if (_gameController)
                _gameController.RegisterToController(this);
        }

        void Update()
		{
			_isGrounded = Physics.CheckSphere(_groundChecker.position, _groundDistance, _ground, QueryTriggerInteraction.Ignore);
			if (!_gameController) return;
			
			if (_gameController.inputs != Vector3.zero)
			{
				transform.forward = _gameController.inputs;
				_anim.SetBool(IS_WALKING, true);
			} 
			else
			{
				_anim.SetBool(IS_WALKING, false);
			}
			
		}

		void FixedUpdate()
        {
			if(_gameController)
            	_rb.MovePosition(_rb.position + _gameController.inputs * _speed * Time.fixedDeltaTime);
			
			if (_characterCanShoot)
				ScanForProjetileShotButtonTrigger();
			
        }

        private void ScanForProjetileShotButtonTrigger()
        {
			_characterCanShoot = false;
			float rightAnalogStickThreshold = 0.2f;

            if (GetProjectileDirection().magnitude >= rightAnalogStickThreshold)
            	ShootProjectile(GetProjectileDirection());

			StartCoroutine(UpdateCanShoot(_weaponSystem.GetPrimaryWeapon().secondsBetweenShots));
        }

        private Vector3 GetProjectileDirection()
        {
			float shouldBeZero = 0; //Keeps the projectile shooting at horizontal plane from start.
			
            return new Vector3(
				_gameController.GetRightStickHorizontal(),
				shouldBeZero,
				_gameController.GetRightStickVertical()
			);
        }

        public void OnButtonPressed(PS4_Controller_Input.Button buttonPressed)
        {
			if (buttonPressed == PS4_Controller_Input.Button.R2)
            	if (_isGrounded) _movement.Jump();

			if (buttonPressed == PS4_Controller_Input.Button.X)
				_movement.Dash();
        }
    }
}

