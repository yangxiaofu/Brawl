
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Weapons;
using Game.Characters;

namespace Game.Core{
	public class WeaponSpawnPoint : MonoBehaviour{
		[SerializeField] float _probabilityOfSpawnPerSecond;
		[SerializeField] protected WeaponConfig _weapon;
		bool _containsSpawnedObject
        {
            get{return this.transform.childCount > 0;}
        }

		void Update()
        {
            if (_containsSpawnedObject) return;

            if(ObjectCanSpawn())
				SpawnObject();
        }

        private bool ObjectCanSpawn()
        {
            var r = Random.Range(0f, 1f);
            var probability = _probabilityOfSpawnPerSecond * Time.deltaTime;

            if (r > probability) return false;

			return true;
        }

        private void SpawnObject()
        {
            var weaponObj = Instantiate(_weapon.GetItemPrefab()) as GameObject; ;
            weaponObj.transform.SetParent(this.transform);
            weaponObj.transform.localPosition = Vector3.zero;
        }

        void OnTriggerEnter(Collider other)
		{
			if(!IsCharacter(other)) return;
			AddItemToCharacterInventory(other.gameObject.GetComponent<Player>());
		}

		protected bool IsCharacter(Collider other)
        {
            if (other.gameObject.GetComponent<Player>()) return true;
            return false;
        }

		protected void AddItemToCharacterInventory(Player character)
        {
            character.gameObject.GetComponent<WeaponSystem>().UpdateWeapon(_weapon);
            Destroy(this.gameObject);
        }
	}

}
