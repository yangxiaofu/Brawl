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

		public Character target;
		
		public float dashDistance{get{return _dashDistance;}}
		protected ProjectileSocket _projectileSocket;
		public ProjectileSocket projectileSocket{
			get{return _projectileSocket;}
		}
		WeaponSystem _weaponSystem;
		Movement _movement;
		Animator _anim;
		Rigidbody _rb;
		EnergySystem _energySystem;
		CapsuleCollider _cc;
		public Rigidbody rigidBody{get{return _rb;}}
		Transform _groundChecker;
        [SerializeField] bool _isGrounded = true;
		bool _characterCanShoot = true;
		const string IS_WALKING = "IsWalking";
		bool _isBlinking = false;
		Renderer _characterRenderer;
		bool _isDead = false;
		NavMeshAgent _agent;

		ControllerBehaviour _controller;
		public void Setup(ControllerBehaviour controller){_controller = controller;}

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
		[HideInInspector] public bool isAttacking = false;
		[Task] public bool IsDead() { return _isDead; }

		void Awake()
        {
            if (!_isBot)
                return;

            SetupNavMeshAgent();
        }


        void Start()
		{
			InitializeVariables();
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
                ScanForProjetileShotButtonTrigger();
                StartCoroutine(UpdateCanShoot(_weaponSystem.primaryWeapon.secondsBetweenShots));
            }

            if (_isBot)
                return;

            UpdatePlayerMovement();
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

		private Vector3 GetProjectileDirection()
        {
			float shouldBeZero = 0; //Keeps the projectile shooting at horizontal plane from start.
			var projectileDirection = Vector3.zero;

			if (!_isBot)
			{
				projectileDirection = new Vector3(
					_controller.GetRightStickHorizontal(),
					shouldBeZero,
					_controller.GetRightStickVertical()
				);
			} 
			return projectileDirection;
        }

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

		private void InitializeVariables()
        {
            _rb = GetComponent<Rigidbody>();
            Assert.IsNotNull(_rb);

			_cc = GetComponent<CapsuleCollider>();
			Assert.IsNotNull(_cc);

            _anim = GetComponent<Animator>();
            Assert.IsNotNull(_anim);

            _groundChecker = GetComponentInChildren<GroundChecker>().transform;
            Assert.IsNotNull(_groundChecker);

            _projectileSocket = GetComponentInChildren<ProjectileSocket>();
			Assert.IsNotNull(_projectileSocket);

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

		private IEnumerator UpdateCanShoot(float delay)
		{
			yield return new WaitForSeconds(delay);
			_characterCanShoot = true;
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

			var p = other.gameObject.GetComponent<Projectile>();
			GetComponent<HealthSystem>().TakeDamage(p.damagePerHit);
			StartCoroutine(Blink(0.1f, 20));	

			_isInvincible = true;
			StartCoroutine(EndInvincible(_invincibleLength));
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
			float ikPositionGoal = 0.5f;
			float yOffset = 1f;
			
			if (_anim == null) return;

			if (_isBot)
			{
				if (target == null) return;
				_anim.SetIKPosition(AvatarIKGoal.RightHand, target.transform.position);
				_anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikPositionGoal);
			} 
			else if (!_isBot)
			{
				var direction = new Vector3(_controller.GetRightStickHorizontal(), 0, _controller.GetRightStickVertical());
				var pointDirection = new Vector3(this.transform.position.x + direction.x, this.transform.position.y + yOffset, this.transform.position.z + direction.z);
				_anim.SetIKPosition(AvatarIKGoal.RightHand, pointDirection);
				_anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikPositionGoal);
			}
			
			
		}
	}
}

