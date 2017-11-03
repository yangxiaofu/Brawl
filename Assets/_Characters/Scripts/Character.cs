using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Game.Items;
using Game.Core;
using Game.Core.ControllerInputs;
using Game.UI;
using Panda;

namespace Game.Characters{
	public class Character : MonoBehaviour {
		[Tooltip("Configure this to make the player a bot or not.")]
		[SerializeField] bool _isBot = false;
		public bool isBot{get{return _isBot;}}
		public void SetAsBot(){ _isBot = true;}
		public void SetAsPlayer(){ _isBot = false;}

		[Space]

		[Header ("Character Movement Parameters")]
		[SerializeField] float _speed = 5f;
		[SerializeField] float _angularSpeed = 120f;
		[SerializeField] float _stoppingDistance = 3f;
		[SerializeField] float _groundDistance = 0.2f;
		[SerializeField] float _jumpHeight = 5f;
		public float jumpHeight{get{return _jumpHeight;}}
		[SerializeField] float _dashDistance = 0.5f;
		[SerializeField] LayerMask _ground;
		[SerializeField] bool _isBeingAttacked = false;
		public void SetBeingAttacked(bool isBeingAttacked) {_isBeingAttacked = isBeingAttacked;}

		[Space] [Header("Energy Consumption")] 
		[SerializeField] protected float _energyConsumeOnJump = 10f;
		[SerializeField] protected float _energyToConsumeOnDash = 50f;

		[Space]
		[SerializeField] protected float _invincibleLength = 5f;
		[SerializeField] protected bool _isInvincible = false;
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
		public float beginAttackThreshold{get{return _beginAttackThreshold;}}
		public Character target;
		public float dashDistance{get{return _dashDistance;}}
		WeaponSystem _weaponSystem;
		Movement _movement;
		Animator _anim;
		Rigidbody _rb;
		EnergySystem _energySystem;
		CapsuleCollider _cc;
		public Rigidbody rigidBody{get{return _rb;}}
		Transform _groundChecker;
        bool _isGrounded = true;
		bool _characterCanShoot = true;
		const string IS_WALKING = "IsWalking";
		bool _isBlinking = false;
		Renderer _characterRenderer;
		bool _isDead = false;
		NavMeshAgent _agent;
		ControllerBehaviour _controller;
		public void Setup(ControllerBehaviour controller){_controller = controller;}
		[HideInInspector] public bool isAttacking = false;
		public bool IsDead() { return _isDead; }

		void Awake()
        {
            if (!_isBot)
                return;

            SetupNavMeshAgent();
        }

        void Start()
        {
            InitializeCharacterVariables();
            InitializeCharacterWeaponSystem();
        }

        private void InitializeCharacterWeaponSystem()
        {
            _weaponSystem.SetupPrimaryWeapon();
            if (_weaponSystem.GunOnStart())
                _weaponSystem.PutGunInHand();
        }

        void Update()
        {
			if (_isBot) 
				return;
			
			CheckGrounded();
			UpdateMovementAnimation();
        }

		void FixedUpdate()
        {
            if (_characterCanShoot)
            {
                if(_weaponSystem.ShotIsFired(_isBot, _controller)){		
					_characterCanShoot = false;
					var secondsToDelayUpdate = _weaponSystem.primaryWeapon.secondsBetweenShots;
					StartCoroutine(UpdateCharacterCanShootAfter(secondsToDelayUpdate));
				}
            }

            if (_isBot)
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
            _rb.MovePosition(_rb.position + _controller.inputs * _speed * Time.fixedDeltaTime);
        }

        private void SetupNavMeshAgent()
        {
            _agent = this.gameObject.AddComponent<NavMeshAgent>();
            _agent.speed = _speed;
            _agent.angularSpeed = _angularSpeed;
            _agent.stoppingDistance = _stoppingDistance;
            _agent.updatePosition = false;
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


		public void OnButtonPressed(PS4_Controller_Input.Button button)
        {
			//TODO: Change to interface for controllers. 
			if (button == PS4_Controller_Input.Button.X)
			{
				if (_isGrounded) Jump();
			} 

			if (button == PS4_Controller_Input.Button.CIRCLE)
			{
				Dash();
			}
        }

		private void InitializeCharacterVariables()
        {
            _rb = GetComponent<Rigidbody>();
            Assert.IsNotNull(_rb);

			_cc = GetComponent<CapsuleCollider>();
			Assert.IsNotNull(_cc);

            _anim = GetComponent<Animator>();
            Assert.IsNotNull(_anim);

            _groundChecker = GetComponentInChildren<GroundChecker>().transform;
            Assert.IsNotNull(_groundChecker);

            _weaponSystem = GetComponent<WeaponSystem>();
			Assert.IsNotNull(_weaponSystem);

			_energySystem = GetComponent<EnergySystem>();
			Assert.IsNotNull(_energySystem);

			_characterRenderer = GetComponentInChildren<Renderer>();
			Assert.IsNotNull(_characterRenderer);
			
			_movement = new Movement(this);
        }

		public bool Jump()
		{
			if (!_energySystem.HasEnergy(_energyConsumeOnJump)) 
				return false;

			_energySystem.ConsumeEnergy(_energyConsumeOnJump);
			_movement.Jump();

			return true;
		}

		public bool Dash()
		{
			if(!_energySystem.HasEnergy(_energyToConsumeOnDash)) 
				return false;

			_energySystem.ConsumeEnergy(_energyToConsumeOnDash); 
			_movement.Dash();
			return true;
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

		void OnCollisionEnter(Collision other)
		{
			if (!other.gameObject.GetComponent<Projectile>()) 
				return;

			var shootingCharacter = other.gameObject.GetComponent<Projectile>().shootingCharacter;
			if (shootingCharacter == this) 
				return;

			DamageCharacter(other.gameObject.GetComponent<Projectile>());

			float secondsBetweenBlinks = 0.1f;
			int totalNumberOfBlinks = 20;
			StartCoroutine(Blink(secondsBetweenBlinks, totalNumberOfBlinks));

			_isInvincible = true;
			StartCoroutine(EndInvincible(_invincibleLength));
		}

		private void DamageCharacter(Projectile projectile)
		{
			GetComponent<HealthSystem>().TakeDamage(projectile.damagePerHit);
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
			if (_anim == null) return;

			if (_isBot)
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
			else if (!_isBot)
			{
				var direction = new Vector3(_controller.GetRightStickHorizontal(), 0, _controller.GetRightStickVertical());
				if (direction.magnitude > 0.2f)
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

