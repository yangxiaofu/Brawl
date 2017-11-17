using System.Collections;
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
		[SerializeField] protected PhysicMaterial _physicsMaterial;
		[SerializeField] protected Vector3 _center = new Vector3(0, 0.85f, 0);
		[SerializeField] protected float _height = 2f;		

		[Space] [Header("Animator")]
		[SerializeField] protected AnimatorOverrideController _animatorOverrideController;
		public AnimatorOverrideController animatorOverrideController{get{return _animatorOverrideController;}}
		[SerializeField] protected Avatar _avatar;
		[SerializeField] protected AnimatorUpdateMode _animatorUpdateMode;

		[Space] [Header("Character Attributes")]
		protected bool _frozen = false;
		public bool frozen{get{return _frozen;}}
		public bool freeze{
			get{return _frozen;}
			set{_frozen = value;}
		}
		protected WeaponSystem _weaponSystem;
		EnergySystem _energySystem;
		public EnergySystem energySystem{get{return _energySystem;}}
		protected Animator _anim;
		protected CapsuleCollider _cc;
		protected CharacterLogic _characterLogic;
		public CharacterLogic logic {get{return _characterLogic;}}
		protected bool _characterCanShoot = true;
		public bool characterCanShoot{get{return _characterCanShoot;}}
        protected void InitializeCharacterVariables()
        {
            _weaponSystem = GetComponent<WeaponSystem>();
            Assert.IsNotNull(_weaponSystem);

            _energySystem = GetComponent<EnergySystem>();
            Assert.IsNotNull(_energySystem);
        }
		protected void DamageCharacter(ProjectileBehaviour projectile)
		{
			GetComponent<HealthSystem>().TakeHit();
		}

        protected void LookAtTarget(Vector3 targetPos)
        {
			float lookAtWeight = 1;
            _anim.SetLookAtWeight(lookAtWeight);
            _anim.SetLookAtPosition(targetPos);
        }

    }
}

