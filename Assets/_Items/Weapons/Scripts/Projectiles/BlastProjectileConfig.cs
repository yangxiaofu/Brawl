using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
    [CreateAssetMenu(menuName = "Game/Projectile/Blast")]
    public class BlastProjectileConfig : ProjectileConfig
    {
        [Header("Blast Projectile Specific")]
        [SerializeField] float _blastRadius = 2f;
        public float blastRadius{get{return _blastRadius;}}
        [Tooltip("This is the amount of time it takes for the grenade to explode once it hits any collider.")]
        [SerializeField] float _delayBeforeExplosion = 1f;
        public float delayBeforeExplosion{get{return _delayBeforeExplosion;}}

        public override void AddComponentTo(GameObject projectileGameObject, ProjectileArgs args)
        {
            var behaviour = projectileGameObject.AddComponent<GrenadeProjectile>();
            behaviour.Setup(args);
        }
    }
}

