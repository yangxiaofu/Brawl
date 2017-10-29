using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Weapons;
using Game.Characters;

namespace Game.Core{
	[ExecuteInEditMode]
	public class ItemPickup : MonoBehaviour {
		[SerializeField] protected WeaponConfig _weapon;

		void Update()
        {
            if (_weapon == null) return;

            DestroyChildren();

            InstantiateItemPrefab();
        }

		void OnTriggerEnter(Collider other)
        {
            if(!IsCharacter(other)) return;
            
            AddItemToCharacterInventory(other.GetComponent<Player>());
        }

        protected void AddItemToCharacterInventory(Player character)
        {
            character.gameObject.GetComponent<WeaponSystem>().UpdateWeapon(_weapon);
            Destroy(this.gameObject);
        }

        protected bool IsCharacter(Collider other)
        {
            if (other.gameObject.GetComponent<Player>()) return true;
            return false;
        }

        private void InstantiateItemPrefab()
        {
            var wpnObj = Instantiate(_weapon.GetItemPrefab());
            wpnObj.transform.SetParent(this.transform);
            wpnObj.transform.localPosition = Vector3.zero;
        }

        private void DestroyChildren()
        {
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}

