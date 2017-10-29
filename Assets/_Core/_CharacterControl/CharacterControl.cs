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
		[SerializeField] float _jumpHeight = 5f;
		public float jumpHeight{get{return _jumpHeight;}}
		[SerializeField] float _dashDistance = 0.5f;
		[SerializeField] LayerMask _ground;
		public float dashDistance{get{return _dashDistance;}}

		[Header("Weapons")]
		[SerializeField] GameObject _projectile;
		[SerializeField] float _projectileSpeed = 10f;
		[SerializeField] float _secondsBetweenShots = 1f;
		ProjectileSocket _projectileSocket;
		Movement _movement;
		Rigidbody _rb;
		public Rigidbody rb{get{return _rb;}}
		Transform _groundChecker;
        bool _isGrounded = true;
		bool _canShoot = true;

		void Awake()
		{
			Assert.IsNotNull(
				_gameController, 
				"A game controller needs to be referenced in the character control."
			);
		}
		void Start()
		{
			_rb = GetComponent<Rigidbody>();
			Assert.IsNotNull(_rb);

			_groundChecker = GetComponentInChildren<GroundChecker>().transform;
			Assert.IsNotNull(_groundChecker);

			_gameController.RegisterToController(this);
			_movement = new Movement(this);
			_projectileSocket = GetComponentInChildren<ProjectileSocket>();
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
			
			if (_canShoot){
				ScanForProjectileShot();
				StartCoroutine(UpdateCanShoot(_secondsBetweenShots));
			}
            
        }

		IEnumerator UpdateCanShoot(float delay)
		{
			yield return new WaitForSeconds(delay);
			_canShoot = true;
		}

        private void ScanForProjectileShot()
        {
			_canShoot = false;
			float rightAnalogStickThreshold = 0.2f;

            if (GetProjectileDirection().magnitude >= rightAnalogStickThreshold)
            	ShootProjectile(GetProjectileDirection());
        }

        private void ShootProjectile(Vector3 direction)
        {

			var p = Instantiate(
				_projectile, 
				_projectileSocket.transform.position,
				Quaternion.identity
			) as GameObject;
			
			p.GetComponent<Rigidbody>().AddForce(direction * _projectileSpeed, ForceMode.VelocityChange);
        }

        private Vector3 GetProjectileDirection()
        {
            return new Vector3(
				_gameController.GetRightStickHorizontal(),
				0,
				_gameController.GetRightStickVertical()
			);
        }

        public void OnButtonPressed(PS4_Controller_Input.Button buttonPressed)
        {
			if (buttonPressed == PS4_Controller_Input.Button.X)
            	if (_isGrounded) _movement.Jump();

			if (buttonPressed == PS4_Controller_Input.Button.R2)
				_movement.Dash();
        }
    }
}

