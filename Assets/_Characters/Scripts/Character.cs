﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Game.Weapons;
using Game.Core;
using Game.Core.ControllerInputs;
using System;

namespace Game.Characters
{
    public class Character : MonoBehaviour 
	{
		[Space][Header("Capsule Collider")]
		[SerializeField] PhysicMaterial _physicsMaterial;
		[SerializeField] Vector3 _center = new Vector3(0, 0.5321544f, 0);
		[SerializeField] float _height = 1.6677024f;		
		bool _isBeingAttacked = false;
		public void SetBeingAttacked(bool isBeingAttacked) {_isBeingAttacked = isBeingAttacked;}
		[Space] [Header("Animator")]
		[SerializeField] AnimatorOverrideController _animatorOverrideController;
		[SerializeField] Avatar _avatar;
		[SerializeField] AnimatorUpdateMode _animatorUpdateMode;
		[Space]
		[SerializeField] protected float _invincibleTimeLength = 5f;



        protected bool _isInvincible = false;
		
		[Space]
		[Header("Kicking Attributes")]
		[SerializeField] float _kickForce = 50f; //TODO: Consider moving this out to weapn Systems? 
		[SerializeField] float _maximumKickDistance = 1f;
		[SerializeField] float _energyConsumedOnKick = 35f;


		[Header("Bot Specific")]
		[Tooltip("This is the distance in which the enemy will stop in front of the player and start shooting. ")]
		[SerializeField] float _maxShootingDistance = 10f;
		public float maxShootingDistance{get{return _maxShootingDistance;}}
		bool _frozen = false;
		public bool frozen{get{return _frozen;}}
		[HideInInspector] public bool isBot = false; //this is set on the controller piece. 

		public bool freeze{
			get{return _frozen;}
			set{_frozen = value;}
		}

		[HideInInspector] public Character target;
		WeaponSystem _weaponSystem;
		Animator _anim;
		EnergySystem _energySystem;
		public EnergySystem energySystem{get{return _energySystem;}}
		CapsuleCollider _cc;
		bool _characterCanShoot = true;
		public bool characterCanShoot{get{return _characterCanShoot;}}
		bool _isBlinking = false;
		Renderer _characterRenderer;
		bool _isDead = false;
		public bool isDead{get{return _isDead;}}
		ControllerBehaviour _controller;
		public ControllerBehaviour controller{get{return _controller;}}
		public void Setup(ControllerBehaviour controller){_controller = controller;}
		[HideInInspector] public bool isAttacking = false;
		Movement _movement;
		CharacterLogic _characterLogic;
		public CharacterLogic logic {get{return _characterLogic;}}
		
		void Awake()
        {
            SetupCapsuleCollider();
			SetupAnimator();				
        }

        void Start()
        {
            InitializeCharacterVariables(); 

			_movement = GetComponent<Movement>();

			_weaponSystem.InitializeWeaponSystem();
			_characterLogic = new CharacterLogic(this);

			Assert.IsFalse(_frozen, "Frozen should be checked as false on start");
        }

        void Update()
        {
			if (isBot) 
				return;

			if (_characterCanShoot)
            {
                if(_weaponSystem.ShotIsFired(isBot, _controller))
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
		
		public void OnButtonUp(PS4_Controller_Input.Button button)
        {
            if (button == PS4_Controller_Input.Button.SQUARE)
			{
				_weaponSystem.UsePowerWeapon();
			}
        }

		public void OnButtonDown(PS4_Controller_Input.Button button)
        {
			if (_frozen)
				return;

			//TODO: Change to interface for controllers. 
			if (button == PS4_Controller_Input.Button.X)
			{
				if (_movement.isGrounded) _movement.Jump();
			} 

			if (button == PS4_Controller_Input.Button.CIRCLE)
			{
				Dash();
			}

			if (button == PS4_Controller_Input.Button.SQUARE)
			{
				_weaponSystem.ChargePowerWeapon();
			}

			if (button == PS4_Controller_Input.Button.TRIANGLE)
			{
				_weaponSystem.AttemptSpecialAbility();
			}

			if (button == PS4_Controller_Input.Button.R2)
			{
				AttemptKick();
			}
        }

		private void AttemptKick()
		{
			if(!_energySystem.HasEnergy(_energyConsumedOnKick))
					return;

			_energySystem.ConsumeEnergy(_energyConsumedOnKick);

			KickItem();
		}

		private void KickItem()
		{
			RaycastHit hit;
			
			if (Physics.Raycast(this.transform.position, transform.forward, out hit, _maximumKickDistance))
			{
				var kickableItem = hit.collider.gameObject.GetComponent<Kickable>();
				
				if (kickableItem)
				{
					var directionalForce = transform.forward * _kickForce;
					kickableItem.GetComponent<Rigidbody>().AddForce(directionalForce, ForceMode.Impulse);
					var forceDirection = (kickableItem.transform.position - this.transform.position).normalized;
					kickableItem.Setup(forceDirection);
				}
			}
		}
		
        private void InitializeCharacterVariables()
        {
            
            _weaponSystem = GetComponent<WeaponSystem>();
            Assert.IsNotNull(_weaponSystem);

            _energySystem = GetComponent<EnergySystem>();
            Assert.IsNotNull(_energySystem);

            _characterRenderer = GetComponentInChildren<Renderer>();
            Assert.IsNotNull(_characterRenderer);
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

        public bool Jump()
		{	
			return _movement.Jump();
		}

		public bool Dash()
		{
			return _movement.Dash();
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

		private void DamageCharacter(ProjectileBehaviour projectile)
		{
			var damage = projectile.GetComponent<BlastBehaviour>().GetDamage();
			GetComponent<HealthSystem>().TakeHit();
		}

		private IEnumerator EndInvincible(float delay)
		{
			yield return new WaitForSeconds(delay);

			_isInvincible = false;			
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(this.transform.position, _maxShootingDistance);
		}

		void OnAnimatorIK(int layerIndex)
		{
			if (_frozen)
				return;

			if (_anim == null) 
				return;

			if (!_controller) 
				return;
				
			if (isBot)
			{
				if (target != null)
                {
                    LookAtTarget(target.transform.position);
                    SetIKPosition(target.transform.position);
                }
                else
                {
                    ResetLookAtWeight();
                }
            } 
			else 
			{
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

        private void LookAtTarget(Vector3 targetPos)
        {
			float lookAtWeight = 1;
            _anim.SetLookAtWeight(lookAtWeight);
            _anim.SetLookAtPosition(targetPos);
        }

    }
}

