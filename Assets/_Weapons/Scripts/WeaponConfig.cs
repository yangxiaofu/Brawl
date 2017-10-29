using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons{
	[CreateAssetMenu(menuName = "Game/Weapon")]
	public class WeaponConfig : ScriptableObject {
		[SerializeField] GameObject _itemPrefab;
		[SerializeField] GameObject _projectilePrefab;
		[SerializeField] float _projectileSpeed = 10f;
		public float projectileSpeed{
			get{return _projectileSpeed;}
		}
		[SerializeField] float _secondsBetweenShots = 1f;
		public float secondsBetweenShots{
			get{return _secondsBetweenShots;}
		}

		public GameObject GetItemPrefab()
		{
			return _itemPrefab;
		}

		public GameObject GetProjectilePrefab()
		{
			return _projectilePrefab;
		}
		
	}
}

