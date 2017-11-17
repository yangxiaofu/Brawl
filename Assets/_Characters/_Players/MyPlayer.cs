using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Weapons;
using Game.Core.ControllerInputs;
using Game.Core;

namespace Game.Characters
{
	public class MyPlayer : Character {
		[Tooltip("This is the amount of time that the player is invincible to hits after the first collision hit.")]
		[SerializeField] float _invincibleTimeLength = 5f;
        bool _isInvincible = false;
		bool _isBlinking = false;
		Renderer _characterRenderer;
		ControllerBehaviour _controller;
		public ControllerBehaviour controller{get{return _controller;}}
		public void Setup(ControllerBehaviour controller){_controller = controller;}
		
		Movement _movement;

		void Awake()
        {
            SetupCapsuleCollider();
			SetupAnimator();				

			InitializeCharacterVariables(); 
        }

		public void OnButtonUp(PS4_Controller_Input.Button button)
        {
            if (button == PS4_Controller_Input.Button.SQUARE)
			{
				_weaponSystem.UsePowerWeapon();
			}
        }

		public bool Dash()
		{
			return _movement.Dash();
		}

		private void SetupAnimator()
        {
            _anim = gameObject.AddComponent<Animator>();
			Assert.IsNotNull(_anim, "You have no animator in the game object scene.");
            _anim.runtimeAnimatorController = _animatorOverrideController;
			_anim.avatar = _avatar;
			_anim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
			_anim.updateMode = _animatorUpdateMode;
        }

		private void SetupCapsuleCollider()
        {
            _cc = gameObject.AddComponent<CapsuleCollider>();
            _cc.material = _physicsMaterial;
			_cc.center = _center;
			_cc.height = _height;
            Assert.IsNotNull(_cc);
        }

		public void OnButtonDown(PS4_Controller_Input.Button button)
        {
			if (_frozen)
				return;

			//TODO: Change to interface for controllers. 
			if (button == PS4_Controller_Input.Button.X)
			{
				if (_movement.isGrounded) 
					_movement.Jump();
			} 

			if (button == PS4_Controller_Input.Button.CIRCLE)
			{
				Dash();
			}

			if (button == PS4_Controller_Input.Button.SQUARE)
			{
				_weaponSystem.AttemptPowerWeaponCharge();
			}

        }

		void Start()
		{
			 _characterRenderer = GetComponentInChildren<Renderer>();
            Assert.IsNotNull(_characterRenderer);

			_movement = GetComponent<Movement>();
			_weaponSystem.InitializeWeaponSystem();
			_characterLogic = new CharacterLogic(this);

		}

		void Update()
		{
			if (_characterCanShoot)
            {
                if(_weaponSystem.ShotIsFired(_controller))
				{		
					_characterCanShoot = false;
					var secondsToDelayUpdate = (_weaponSystem.primaryWeapon as RangeWeaponConfig).secondsBetweenShots;
					StartCoroutine(UpdateCharacterCanShootAfter(secondsToDelayUpdate));
				}
            }
		}

		private IEnumerator UpdateCharacterCanShootAfter(float delay)
		{
			yield return new WaitForSeconds(delay);
			_characterCanShoot = true;
			yield return null;
		}


		private IEnumerator EndInvincible(float delay)
		{
			yield return new WaitForSeconds(delay);

			_isInvincible = false;			
		}

		void OnCollisionEnter(Collision collision)
        {
            DetermineIfCharacterDamagedFrom(collision);
        }

		private void DetermineIfCharacterDamagedFrom(Collision other)
        {
			if (_characterLogic.WillTakeDamageFrom(other.gameObject)) 
			{
				DamageCharacter(other.gameObject.GetComponent<ProjectileBehaviour>());

				var secondsBetweenBlinks = 0.1f;
				var totalNumberOfBlinks = 20;

				StartCoroutine(Blink(secondsBetweenBlinks, totalNumberOfBlinks));

				_isInvincible = true;
				StartCoroutine(EndInvincible(_invincibleTimeLength));
			}         
        }


        private IEnumerator Blink(float seconds, int numBlinks)
		{
			if (!_isBlinking)
			{
				_isBlinking = true;
				for (int i=0; i<numBlinks*2; i++) 
				{
					_characterRenderer.enabled = !_characterRenderer.enabled;	
					yield return new WaitForSeconds(seconds);
				}
				_characterRenderer.enabled = true;
				_isBlinking = false;
			}
		}

		void OnAnimatorIK(int layerIndex)
		{
			if (_frozen)
				return;

			if (_anim == null) 
				return;

			if (!_controller) 
				return;
				

			var direction = new Vector3(
				_controller.GetRightStickHorizontal(), 
				0, 
				_controller.GetRightStickVertical()
			);

			var rightStickThreshold = 0.2f;
			if (direction.magnitude > rightStickThreshold)
			{
				float yOffset = 1f;
				var pointDirection = new Vector3(
					this.transform.position.x + direction.x, 
					this.transform.position.y + yOffset, 
					this.transform.position.z + direction.z
				);
				LookAtTarget(pointDirection);
				SetIKPosition(pointDirection);
			} 
			else 
			{
				ResetLookAtWeight();
			}
		}

		 private void ResetLookAtWeight()
        {
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            _anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            _anim.SetLookAtWeight(0);
        }


        private void SetIKPosition(Vector3 target)
        {
			float ikPositionGoal = 1.0f;
            _anim.SetIKPosition(AvatarIKGoal.RightHand, target);
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikPositionGoal);
        }
	}

}
