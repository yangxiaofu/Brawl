using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Items;
using Game.Core;
using Game.UI;
using Panda;

namespace Game.Characters{
	public abstract class Character : MonoBehaviour {
		[Header ("Character Movement Parameters")]
		[SerializeField] protected float _speed = 5f;
		[SerializeField] protected float _groundDistance = 0.2f;
		[SerializeField] protected float _jumpHeight = 5f;
		public float jumpHeight{get{return _jumpHeight;}}
		[SerializeField] protected float _dashDistance = 0.5f;
		[SerializeField] protected LayerMask _ground;
		[SerializeField] bool _isBeingAttacked = false;
		public void SetBeingAttacked(bool isBeingAttacked) {_isBeingAttacked = isBeingAttacked;}

		[Space] [Header("Energy Consumption")] 
		[SerializeField] protected float _energyConsumeOnJump = 10f;
		[SerializeField] protected float _energyToConsumeOnDash = 50f;

		[Space]
		[SerializeField] protected float _invincibleLength = 5f;
		public float dashDistance{get{return _dashDistance;}}
		protected ProjectileSocket _projectileSocket;
		protected WeaponSystem _weaponSystem;
		protected Movement _movement;
		protected Animator _anim;
		protected Rigidbody _rb;
		protected EnergySystem _energySystem;
		CapsuleCollider _cc;
		public Rigidbody rigidBody{get{return _rb;}}
		protected Transform _groundChecker;
        protected bool _isGrounded = true;
		protected bool _characterCanShoot = true;
		protected const string IS_WALKING = "IsWalking";
		protected bool _isBlinking = false;
		protected Renderer _characterRenderer;
		protected bool _isDead = false;
		protected PlayerUI _ui;
		public PlayerUI uI{get{return _ui;}}
		public void SetupUI(PlayerUI ui){ _ui = ui;}

		[Task] public bool IsDead() { return _isDead; }

		protected void InitializeVariables()
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

		[Task]
		public bool Jump()
		{
			if (!_energySystem.HasEnergy(_energyConsumeOnJump)) 
				return false;

			_energySystem.ConsumeEnergy(_energyConsumeOnJump);
			_movement.Jump();

			return true;
		}

		[Task]
		public bool Dash()
		{
			if(!_energySystem.HasEnergy(_energyToConsumeOnDash)) 
				return false;

			_energySystem.ConsumeEnergy(_energyToConsumeOnDash); _movement.Dash();
			return true;
		}

		protected IEnumerator UpdateCanShoot(float delay)
		{
			yield return new WaitForSeconds(delay);
			_characterCanShoot = true;
		}
		
        protected IEnumerator Blink(float seconds, int numBlinks)
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

		public void OnCollisionEnter(Collision other)
		{
			if (!other.gameObject.GetComponent<Projectile>()) 
				return;

			var p = other.gameObject.GetComponent<Projectile>();
			GetComponent<HealthSystem>().TakeDamage(p.damagePerHit);
			StartCoroutine(Blink(0.1f, 20));	
		}

		public abstract void OnCollisionEnterAction(Collision other);

		
	}
}

