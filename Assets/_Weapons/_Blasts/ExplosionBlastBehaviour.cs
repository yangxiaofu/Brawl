using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;

namespace Game.Weapons
{
	public class ExplosionBlastBehaviour : BlastBehaviour 
    {   
		void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, _blastConfig.blastRadius);
        }

        public override void DoBlastSpecificBehaviourToEnemies()
        {
            for(int i = 0; i < _enemiesImpactedOnBlast.Count; i++)
            {
                ApplyBlastForceTo(_enemiesImpactedOnBlast[i].gameObject);
            }

            for(int i = 0; i < _objectsImpactedOnBlast.Count; i++)
            {
                ApplyBlastForceTo(_objectsImpactedOnBlast[i]);
            }
        }
    }
}

