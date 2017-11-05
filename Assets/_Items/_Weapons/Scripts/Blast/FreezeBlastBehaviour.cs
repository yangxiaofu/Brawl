using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items
{
    public class FreezeBlastBehaviour : BlastBehaviour
    {
        public override void DoBlastSpecificBehaviour()
        {
            foreach(Character character in _charactersImpactedOnBlast)
			{
				Freeze(character);
			}
        }

		private void Freeze(Character character)
		{
			print("Freeze character for " + (_blastConfig as FreezeBlastConfig).freezeTime + " seconds");
		}

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, _blastConfig.blastRadius);
        }
    }
}


