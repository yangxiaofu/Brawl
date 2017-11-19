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
		[SerializeField] protected Avatar _avatar;
		[SerializeField] protected AnimatorUpdateMode _animatorUpdateMode;
		protected bool _frozen = false;
		public bool frozen{get{return _frozen;}}
		protected WeaponSystem _weaponSystem;
		EnergySystem _energySystem;
		public EnergySystem energySystem{get{return _energySystem;}}
		protected Animator _anim;
		protected CapsuleCollider _cc;
		protected CharacterLogic _characterLogic;
		public CharacterLogic logic {get{return _characterLogic;}}
		protected bool _characterCanShoot = true;
		public bool characterCanShoot{get{return _characterCanShoot;}}
		bool _isDead = false;
		public bool isDead {get{return _isDead;}}

        const string DEAD_TRIGGER = "Dead";
        protected void InitializeCharacterVariables()
        {
            _weaponSystem = GetComponent<WeaponSystem>();
            Assert.IsNotNull(_weaponSystem);

            _energySystem = GetComponent<EnergySystem>();
            Assert.IsNotNull(_energySystem);
        }
		protected void DamageCharacter(ProjectileBehaviour projectile)
		{
			PlayBloodEffect();
			GetComponent<HealthSystem>().DealDamage(projectile.GetDamage()); //TODO: Refactor this out later. 
		}

        protected void LookAtTarget(Vector3 targetPos)
        {
			float lookAtWeight = 1;
			Assert.IsNotNull(_anim, "You animator is not a comonent of " + this.gameObject);
            _anim.SetLookAtWeight(lookAtWeight);
            _anim.SetLookAtPosition(targetPos);
        }

		public void KillCharacter()
        {
            _isDead = true;
            GetComponent<Animator>().SetTrigger(DEAD_TRIGGER);
            GetComponent<NavMeshAgent>().enabled = false;
            PlayBloodEffect();
        }

        private void PlayBloodEffect()
        {
            var killParticleEffectObject = Instantiate(GetComponent<HealthSystem>().killParticleEffectPrefab, this.transform.position, this.transform.rotation) as GameObject;
            var particleSystem = killParticleEffectObject.GetComponent<ParticleSystem>();
            particleSystem.Play();
        }

		protected void ResetLookAtWeight()
        {
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            _anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            _anim.SetLookAtWeight(0);
        }

        protected void SetIKPosition(Vector3 target)
        {
			float ikPositionGoal = 1.0f;
            _anim.SetIKPosition(AvatarIKGoal.RightHand, target);
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikPositionGoal);
        }
    }
}

