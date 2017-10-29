using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core.ControllerInputs;
using Game.Weapons;
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
		ProjectileSocket _projectileSocket;
		WeaponSystem _weaponSystem;
		Movement _movement;
		Rigidbody _rb;
		Animator _anim;
		public Rigidbody rigidBody{get{return _rb;}}
		Transform _groundChecker;
        bool _isGrounded = true;
		bool _canShoot = true;
		const string IS_WALKING = "IsWalking";

		void Start()
		{
			_rb = GetComponent<Rigidbody>();
			Assert.IsNotNull(_rb);

			_anim = GetComponent<Animator>();
			Assert.IsNotNull(_anim);

			_groundChecker = GetComponentInChildren<GroundChecker>().transform;
			Assert.IsNotNull(_groundChecker);

			if (_gameController)
				_gameController.RegisterToController(this);

			_movement = new Movement(this);
			_projectileSocket = GetComponentInChildren<ProjectileSocket>();

			_weaponSystem = GetComponent<WeaponSystem>();
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
			
			if (_canShoot){
				ScanForProjectileShot();

				StartCoroutine(
					UpdateCanShoot(
						GetComponent<WeaponSystem>()
							.GetPrimaryWeapon().secondsBetweenShots));
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
				GetComponent<WeaponSystem>().GetPrimaryWeapon().GetProjectilePrefab(), 
				_projectileSocket.transform.position,
				Quaternion.identity
			) as GameObject;
			
			p.GetComponent<Rigidbody>().AddForce(
				direction * _weaponSystem.GetPrimaryWeapon().projectileSpeed, 
				ForceMode.VelocityChange
			);
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

