using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Core.ControllerInputs;

namespace Game.Core{
	public class Movement : MonoBehaviour {
		[Header ("Character Movement Parameters")]
		[SerializeField] float _speed = 5f;
		[SerializeField] float _jumpHeight = 5f;
		[SerializeField] float _dashDistance = 0.5f;
		[SerializeField] float _groundDistance = 0.2f;

		[Space][Header("Rigid Body Parameters")]
		[SerializeField] float _mass = 20f;
		[SerializeField] float _drag = 10f;
		[SerializeField] float _angularSpeed = 8f;
		[Space]
		[SerializeField] LayerMask _ground;
		bool _isGrounded = true;
		public bool isGrounded{get{return _isGrounded;}}
		Rigidbody _rb;
		Character _character;
		Transform _groundChecker;
		Animator _anim;
		const string IS_WALKING = "IsWalking";
		
		void Start () 
		{
			SetupRigidBody();

			_character = GetComponent<Character>();
			_anim = GetComponent<Animator>();
			Assert.IsNotNull(_anim);
			_groundChecker = GetComponentInChildren<GroundChecker>().transform;
            Assert.IsNotNull(_groundChecker);
		}
		
		// Update is called once per frame
		void Update ()
        {
            CheckIfGrounded();
			UpdateMovementAnimation();
        }

		 void FixedUpdate()
		{
			if(!_character.logic.CanMove(_character.frozen, _character.controller ? true : false))
				return;
			
			if(UpdatePlayerMovement())
			{
				UpdateCharacterFacingDirection();
			}
				
		}

        private void UpdateCharacterFacingDirection()
        {
            if (_character.controller.GetMovementInputs() != Vector3.zero)
                transform.forward = _character.controller.GetMovementInputs();
        }

		private void UpdateMovementAnimation()
        {
			if(_character.logic.CanMove(_character.frozen, _character.controller))
			{
				float animationThreshold = 0.2f;
            
				if (_character.controller.GetMovementInputs().magnitude > animationThreshold)
				{
					_anim.SetBool(IS_WALKING, true);
				}
				else
				{
					_anim.SetBool(IS_WALKING, false);
				}
			}
        }
		private bool UpdatePlayerMovement()
        {
			if (_character.controller.GetMovementInputs().magnitude > 0.3f){
				var movePos = _rb.position + _character.controller.GetMovementInputs() * _speed * Time.fixedDeltaTime;
            	_rb.MovePosition(movePos);
				return true;
				
			}

			return false;
        }

		private void SetupRigidBody()
        {
            _rb = gameObject.AddComponent<Rigidbody>();
            _rb.angularDrag = _angularSpeed;
            _rb.drag = _drag;
            _rb.mass = _mass;
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            _rb.useGravity = true;
            _rb.isKinematic = false;

            Assert.IsNotNull(_rb);
        }

		private void CheckIfGrounded()
        {
            _isGrounded = UnityEngine.Physics.CheckSphere(
				_groundChecker.position,
				_groundDistance,
				_ground,
				QueryTriggerInteraction.Ignore
			);
        }

		public bool Jump()
		{	
			if (_character.frozen) 
				return false;

			if (!_character.energySystem.HasEnergy(_character.energySystem.energyToConsumeOnJump)) 
				return false;

			_character.energySystem.ConsumeEnergy(_character.energySystem.energyToConsumeOnJump);
			
			var physics = new GamePhysics();
			var appliedForce = physics.GetAppliedForceWithGravity(Vector3.up, _jumpHeight);

			_rb.AddForce
			(
				appliedForce, ForceMode.VelocityChange
			);

			return true;
		}

		public bool Dash()
		{
			if (_character.frozen) 
				return false;

			if(!_character.energySystem.HasEnergy(_character.energySystem.energyToConsumeOnDash)) 
				return false;

			_character.energySystem.ConsumeEnergy(_character.energySystem.energyToConsumeOnDash); 

			Vector3 dashVelocity = Vector3.Scale(this.transform.forward, _dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime)));
            _rb.AddForce(dashVelocity, ForceMode.VelocityChange);
			
			return true;
		}
	}
}


