using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Items{
	public class ExplosionBehaviour : MonoBehaviour {
		[SerializeField] AnimatorOverrideController _animatorOverrideController;
		public AnimatorOverrideController animatorOverrideController;

		public const string DEFAULT_EXPLOSION = "DEFAULT_EXPLOSION";

		void Start()
		{
			Assert.IsNotNull(_animatorOverrideController);

			SetupAnimator();
		}

		public void SetupAnimator()
		{
			Animator anim = GetComponentInChildren<Animator>();
			anim.runtimeAnimatorController = _animatorOverrideController;
		}

        public float GetBlastDuration()
        {
            return _animatorOverrideController[DEFAULT_EXPLOSION].length;
        }
    }
}

