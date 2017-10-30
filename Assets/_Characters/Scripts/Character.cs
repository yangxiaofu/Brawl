﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Weapons;
using Game.Core;
using Panda;

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
		CapsuleCollider _cc;
		public Rigidbody rigidBody{get{return _rb;}}
		protected Transform _groundChecker;
        protected bool _isGrounded = true;
		protected bool _characterCanShoot = true;
		protected const string IS_WALKING = "IsWalking";
		protected bool _isBlinking = false;
		protected Renderer enemyRenderer;
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

			_movement = new Movement(this);
        }

		protected IEnumerator UpdateCanShoot(float delay)
		{
			yield return new WaitForSeconds(delay);
			_characterCanShoot = true;
		}
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
					enemyRenderer.enabled = !enemyRenderer.enabled;	
					yield return new WaitForSeconds(seconds);
				}
				enemyRenderer.enabled = true;
				_isBlinking = false;
			}
		}

		[Task]
		public bool IsDead()
		{
			return _isDead;
		}
	
	}
}

