using System.Collections;
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
            ApplyBlastForce();
        }

		private void ApplyBlastForce()
        {
            foreach(Character character in _charactersImpactedOnBlast){
                 var forceDirection = (character.gameObject.transform.position - this.transform.position).normalized;
                 var force = _blastConfig.blastForce;
                 character.gameObject.GetComponent<Rigidbody>()
                    .AddForce(forceDirection * force, ForceMode.Impulse);
            }
            
        }


    }
}

