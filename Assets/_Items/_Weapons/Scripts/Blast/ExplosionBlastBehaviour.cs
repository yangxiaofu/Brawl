﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;

namespace Game.Items
{
	public class ExplosionBlastBehaviour : BlastBehaviour 
    {
		void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, _blastConfig.blastRadius);
        }

        public override void DoBlastSpecificBehaviour()
        {
            foreach(Character character in _charactersImpactedOnBlast)
            {
                ApplyBlastForceTo(character);
            }
        }

		private void ApplyBlastForceTo(Character character)
        {
            var forceDirection = (character.gameObject.transform.position - this.transform.position).normalized;
            var rigidBody = character.gameObject.GetComponent<Rigidbody>();                 
            rigidBody.AddForce(forceDirection * _blastConfig.blastForce, ForceMode.Impulse);
        }
    }
}

