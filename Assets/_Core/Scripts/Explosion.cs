using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class Explosion {

		private readonly Vector3 pos;
		private readonly float radius;
		private readonly float multiplier = 1f;
		private readonly float explosionForce = 4f;

		public Explosion(Vector3 position, float explosionRadius, float explosionForce){
			pos = position;
			radius = explosionRadius;
			this.explosionForce = explosionForce;
		}

		public void Explode()
        {
            var cols = Physics.OverlapSphere(pos, radius);
            var rigidBodies = new List<Rigidbody>();

            foreach (var col in cols)
            {
                if (col.attachedRigidbody != null && !rigidBodies.Contains(col.attachedRigidbody))
                {
                    rigidBodies.Add(col.attachedRigidbody);
                }
            }

            foreach (var rb in rigidBodies)
            {
                rb.AddExplosionForce(explosionForce * multiplier, pos, radius, 1 * multiplier, ForceMode.Impulse);
            }
        }
	}

}
