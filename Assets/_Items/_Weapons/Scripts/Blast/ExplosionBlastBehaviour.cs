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
            for(int i = 0; i < _charactersImpactedOnBlast.Count; i++)
            {
                ApplyBlastForceTo(_charactersImpactedOnBlast[i].gameObject);
            }

            for(int i = 0; i < _objectsImpactedOnBlast.Count; i++)
            {
                ApplyBlastForceTo(_objectsImpactedOnBlast[i]);
            }
        }

		
    }
}

