using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;

namespace Game.Items{
	public class ExplosionBlastBehaviour : BlastBehaviour 
    {
		public override int PerformBlast()
        {
			var totalHits = 0;
            var hits = Physics.SphereCastAll(this.transform.position, _blastConfig.blastRadius, Vector3.up);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<Character>())
                {
                    var hitCharacter = hit.collider.gameObject.GetComponent<Character>();
                    ApplyForce(hitCharacter.gameObject);
                    if (hitCharacter != _character)
                    {
						totalHits += 1;
                    }
                }
            }

            return totalHits;
        }

		void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, _blastConfig.blastRadius);
        }
	}
}

