using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Weapons;
using Game.Core;
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

		protected IEnumerator UpdateCanShoot(float delay)
		{
			yield return new WaitForSeconds(delay);
			_characterCanShoot = true;
		}

		[Task]
		protected void ShootProjectile(Vector3 direction)
        {
			var projectileObject = Instantiate(
				_weaponSystem.GetPrimaryWeapon().GetProjectilePrefab(),
				_projectileSocket.transform.position,
				Quaternion.identity
			) as GameObject;
			
			projectileObject.GetComponent<Rigidbody>().AddForce(
				direction * _weaponSystem.GetPrimaryWeapon().projectileSpeed, 
				ForceMode.VelocityChange
			);
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
			if (other.gameObject.GetComponent<Projectile>())
			{
				StartCoroutine(Blink(0.1f, 20));
			}			

			
		}
		public abstract void OnCollisionEnterAction(Collision other);

		[Task]
		public bool IsDead()
		{
			return _isDead;
		}
	
	}
}

