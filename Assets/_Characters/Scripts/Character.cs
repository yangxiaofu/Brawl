﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Game.Items;
using Game.Core;
using Game.Core.ControllerInputs;

namespace Game.Characters
{
    public class Character : MonoBehaviour 
	{
		[Header ("Character Movement Parameters")]
		[SerializeField] float _speed = 5f;

		[Space][Header("Rigid Body Parameters")]
		[SerializeField] float _angularSpeed = 8f;
		[SerializeField] float _drag = 10f;
		[SerializeField] float _mass = 20f;
		
		[Space][Header("Capsule Collider")]
		[SerializeField] PhysicMaterial _physicsMaterial;
		[SerializeField] Vector3 _center = new Vector3(0, 0.5321544f, 0);
		[SerializeField] float _height = 1.6677024f;

		[Space][Header("Nav Mesh Agent")]
		[SerializeField] float _stoppingDistance = 3f;
		[SerializeField] float _groundDistance = 0.2f;
		[SerializeField] float _jumpHeight = 5f;
		public float jumpHeight{get{return _jumpHeight;}}
		[SerializeField] float _dashDistance = 0.5f;
		[SerializeField] LayerMask _ground;
		bool _isBeingAttacked = false;
		public void SetBeingAttacked(bool isBeingAttacked) {_isBeingAttacked = isBeingAttacked;}
		[Space] [Header("Animator")]
		[SerializeField] AnimatorOverrideController _animatorOverrideController;
		[SerializeField] Avatar _avatar;

		

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

		[Space]
		[Tooltip("When the amount goes below this, the enemy will run and hide for cover until he has enough health.")]
		[Range(0, 1)]
		[SerializeField] float _inDangerThreshold = 0.2f;
		public float inDangerThreshold {get{return _inDangerThreshold;}}

		[Tooltip("Setting this will make the enemy go from hiding to attacking an enemy.")]
		[Range(0, 1)]
		[SerializeField] float _beginAttackThreshold = 0.8f;
		bool _frozen = false;

		[HideInInspector] public bool isBot = false; //this is set on the controller piece. 

		public bool freeze{
			get{return _frozen;}
			set{_frozen = value;}
		}

		public float beginAttackThreshold{get{return _beginAttackThreshold;}}
		[HideInInspector] public Character target;
		WeaponSystem _weaponSystem;
		Animator _anim;
		Rigidbody _rb;
		EnergySystem _energySystem;
		CapsuleCollider _cc;
		Transform _groundChecker;
        bool _isGrounded = true;
		bool _characterCanShoot = true;
		const string IS_WALKING = "IsWalking";
		bool _isBlinking = false;
		Renderer _characterRenderer;
		bool _isDead = false;
		NavMeshAgent _agent;
		ControllerBehaviour _controller;
		
		public ControllerBehaviour controller{get{return _controller;}}
		public void Setup(ControllerBehaviour controller){_controller = controller;}
		[HideInInspector] public bool isAttacking = false;
		public bool IsDead() { return _isDead; }
		CharacterLogic _characterLogic;
		
		void Awake()
        {
            if (!isBot)
                return;

            SetupNavMeshAgent();
        }

        void Start()
        {
			SetupRigidBody();
            SetupCapsuleCollider();
			SetupAnimator();

            InitializeCharacterVariables(); 

			_weaponSystem.InitializeWeaponSystem();
			_characterLogic = new CharacterLogic(this);

			Assert.IsFalse(_frozen, "Frozen should be checked as false on start");
        }

        void Update()
        {
			if (isBot) 
				return;
			
			CheckIfGrounded();

			UpdateMovementAnimation();
        }

		void FixedUpdate()
        {	
			bool hasController = _controller ? true : false;
			if(!_characterLogic.CanMove(_frozen, hasController))
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

            if (isBot)
                return;

            UpdatePlayerMovement();
        }

		private IEnumerator UpdateCharacterCanShootAfter(float delay)
		{
			yield return new WaitForSeconds(delay);
			_characterCanShoot = true;
			yield return null;
		}
		
        private void UpdatePlayerMovement()
        {
            _rb.MovePosition(_rb.position + _controller.GetMovementInputs() * _speed * Time.fixedDeltaTime);
        }

        private void SetupNavMeshAgent()
        {
            _agent = this.gameObject.AddComponent<NavMeshAgent>();
            _agent.speed = _speed;
            _agent.angularSpeed = _angularSpeed;
            _agent.stoppingDistance = _stoppingDistance;
            _agent.updatePosition = false;
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

		private void UpdateMovementAnimation()
        {
			if(_characterLogic.CanMove(_frozen, _controller))
			{
				float animationThreshold = 0.2f;
            
				if (_controller.GetMovementInputs().magnitude > animationThreshold)
				{
					transform.forward = _controller.GetMovementInputs();
					_anim.SetBool(IS_WALKING, true);
				}
				else
				{
					_anim.SetBool(IS_WALKING, false);
				}
			}
        }

		public void OnButtonPressed(PS4_Controller_Input.Button button)
        {
			if (_frozen)
				return;

			//TODO: Change to interface for controllers. 
			if (button == PS4_Controller_Input.Button.X)
			{
				if (_isGrounded) Jump();
			} 

			if (button == PS4_Controller_Input.Button.CIRCLE)
			{
				Dash();
			}

			if (button == PS4_Controller_Input.Button.SQUARE)
			{
				_weaponSystem.UseSecondaryWeapon();
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
            

            _groundChecker = GetComponentInChildren<GroundChecker>().transform;
            Assert.IsNotNull(_groundChecker);

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
            _anim.runtimeAnimatorController = _animatorOverrideController;
			_anim.avatar = _avatar;
			_anim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
			_anim.updateMode = AnimatorUpdateMode.AnimatePhysics;
            Assert.IsNotNull(_anim);
        }

        private void SetupCapsuleCollider()
        {
            _cc = gameObject.AddComponent<CapsuleCollider>();
            _cc.material = _physicsMaterial;
			_cc.center = _center;
			_cc.height = _height;
            Assert.IsNotNull(_cc);
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

        public bool Jump()
		{	
			if (_frozen) 
				return false;

			if (!_energySystem.HasEnergy(_energySystem.energyToConsumeOnJump)) 
				return false;

			_energySystem.ConsumeEnergy(_energySystem.energyToConsumeOnJump);
			
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
			if (_frozen) return false;

			if(!_energySystem.HasEnergy(_energySystem.energyToConsumeOnDash)) 
				return false;

			_energySystem.ConsumeEnergy(_energySystem.energyToConsumeOnDash); 

			Vector3 dashVelocity = Vector3.Scale(this.transform.forward, _dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _rb.drag + 1)) / -Time.deltaTime)));
            _rb.AddForce(dashVelocity, ForceMode.VelocityChange);
			
			return true;
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
			GetComponent<HealthSystem>().TakeDamage(damage);
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

