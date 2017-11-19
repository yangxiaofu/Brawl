using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons{

	[CreateAssetMenu(menuName = "Game/Projectile")]
	public class ProjectileConfig : ScriptableObject {

		[Tooltip("This is the time it takes to destroy the projectile after it has been instantiated in the game scene.")]
		[SerializeField] GameObject _projectilePrefab;
		public GameObject GetProjectilePrefab() { return _projectilePrefab;}
	}

}
