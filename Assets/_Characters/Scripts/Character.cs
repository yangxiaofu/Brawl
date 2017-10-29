using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Weapons;
using Game.Core;

namespace Game.Characters{
	public class Character : MonoBehaviour {
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
		public Rigidbody rigidBody{get{return _rb;}}
		protected Transform _groundChecker;
        protected bool _isGrounded = true;
		protected bool _characterCanShoot = true;
		protected const string IS_WALKING = "IsWalking";

		protected void InitializeVariables()
        {
            _rb = GetComponent<Rigidbody>();
            Assert.IsNotNull(_rb);

            _anim = GetComponent<Animator>();
            Assert.IsNotNull(_anim);

            _groundChecker = GetComponentInChildren<GroundChecker>().transform;
            Assert.IsNotNull(_groundChecker);

            _movement = new Movement(this);

            _projectileSocket = GetComponentInChildren<ProjectileSocket>();

            _weaponSystem = GetComponent<WeaponSystem>();
        }

		protected IEnumerator UpdateCanShoot(float delay)
		{
			yield return new WaitForSeconds(delay);
			_characterCanShoot = true;
		}

		protected void ShootProjectile(Vector3 direction)
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
	}
}

