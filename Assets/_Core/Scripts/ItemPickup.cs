using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Weapons;

namespace Game.Core{
	[ExecuteInEditMode]
	public class ItemPickup : MonoBehaviour {
		[SerializeField] WeaponConfig _weapon;

		void Update()
        {
            if (_weapon == null) return;
            DestroyChildren();
            InstantiateItemPrefab();
        }

		void OnTriggerEnter(Collider other)
        {
            ScanOfCharacter(other);
        }

        private void ScanOfCharacter(Collider other)
        {
            if (!other.gameObject.GetComponent<CharacterControl>()) return;

            other.gameObject.GetComponent<WeaponSystem>().UpdateWeapon(_weapon);

            Destroy(this.gameObject);
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

