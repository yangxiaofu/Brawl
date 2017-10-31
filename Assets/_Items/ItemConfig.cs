using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	public abstract class ItemConfig : ScriptableObject {
		[SerializeField] protected GameObject _itemPrefab;
		public GameObject GetItemPrefab()
		{
			return _itemPrefab;
		}
		public abstract bool IsWeapon();
	}
}

