using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
    [CreateAssetMenu(menuName = "Game/Projectile/Bullet")]
    public class BulletProjectileConfig : ProjectileConfig
    {
        [SerializeField] float _projectileCollisionForce = 200f;
		public float projectileCollisionForce{get{return _projectileCollisionForce;}}
        
        public override void AddComponentTo(GameObject projectileGameObject, ProjectileArgs args)
        {
            var behaviour = projectileGameObject.AddComponent<BulletProjectile>();
			behaviour.Setup(args);
        }
    }
}

