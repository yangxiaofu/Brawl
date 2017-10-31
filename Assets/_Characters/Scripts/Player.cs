using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core.ControllerInputs;
using Game.Items;
using Game.Core;
using System;

namespace Game.Characters{
	public class Player : Character {
		[SerializeField] bool _isInvincible = false;
		ControllerBehaviour _controller;
		public void Setup(ControllerBehaviour controller){_controller = controller;}
        public void OnButtonPressed(PS4_Controller_Input.Button button)
        {
			if (button == PS4_Controller_Input.Button.X)
			{
				if (_isGrounded) Jump();
			} 

			if (button == PS4_Controller_Input.Button.CIRCLE)
			{
				Dash();
			}
        }
		
        public override void OnCollisionEnterAction(Collision other)
        {
            _isInvincible = true;

			StartCoroutine(EndInvincible(_invincibleLength));
        }

        private void ScanForProjetileShotButtonTrigger()
        {
			_characterCanShoot = false;
			float rightAnalogStickThreshold = 0.2f;

            if (GetProjectileDirection().magnitude >= rightAnalogStickThreshold){
				_weaponSystem.ShootProjectile(
					GetProjectileDirection(), 
					_projectileSocket.transform.position
				);
			}	
        }

       	private Vector3 GetProjectileDirection()
        {
			float shouldBeZero = 0; //Keeps the projectile shooting at horizontal plane from start.
			
            return new Vector3(
				_controller.GetRightStickHorizontal(),
				shouldBeZero,
				_controller.GetRightStickVertical()
			);
        }
		IEnumerator EndInvincible(float delay)
		{
			yield return new WaitForSeconds(delay);

			_isInvincible = false;			
		}

		void Start()
        {
            InitializeVariables();	
        }

        void Update()
        {
            CheckGrounded();
            UpdateMovementAnimation();
        }

		void FixedUpdate()
        {
            _rb.MovePosition(
                _rb.position + 
                _controller.inputs * 
                _speed * 
                Time.fixedDeltaTime
            );
			
			if (_characterCanShoot)
            {
				ScanForProjetileShotButtonTrigger();
				StartCoroutine(UpdateCanShoot(_weaponSystem.primaryWeapon.secondsBetweenShots));
			}
        }

        private void UpdateMovementAnimation()
        {
            float animationThreshold = 0.2f;
            
            if (_controller.inputs.magnitude > animationThreshold)
            {
                transform.forward = _controller.inputs;
                _anim.SetBool(IS_WALKING, true);
            }
            else
            {
                _anim.SetBool(IS_WALKING, false);
            }
        }

        private void CheckGrounded()
        {
            _isGrounded = UnityEngine.Physics.CheckSphere(
				_groundChecker.position,
				_groundDistance,
				_ground,
				QueryTriggerInteraction.Ignore
			);
        }
    }
}

