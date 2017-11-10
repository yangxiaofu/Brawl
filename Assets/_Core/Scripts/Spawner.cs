using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Items;
using Game.Characters;

namespace Game.Core{
	public class Spawner : MonoBehaviour {

		[Header("Spawn Configurations")]
		[SerializeField] List<ItemConfig> _itemsToPotentiallySpawn = new List<ItemConfig>();

		[Tooltip("Adjusts the probability that a item will spawn per second.")]
		[Range(0,1)]
		[SerializeField] float _probabilityOfSpawnPerSecond;

		[Space]
		[Header("Spawn Times")]

		[Tooltip("Adjust the amount of time an item has to spawn.")]
		[Range(0,30)]
		[SerializeField] float _maxSpawnTime = 10f;
		[Range(0, 10)]
		[SerializeField] float _minSpawnTime = 5f;
		GameObject _spawnedItemObject;
		ItemConfig _spawnedItemConfig;
		SpawnerLogic _logic;

		void Start()
        {
            Assert.AreNotEqual(0, _itemsToPotentiallySpawn.Count, "You do not have any items in the item spawner.  Are you sure you do not want this?");

            InitializeBoxCollider();

			_logic = new SpawnerLogic();
        }


		void Update()
		{
			if (_logic.ContainsSpawnedObject(this.gameObject))
				return;

            if(!ObjectCanSpawn()) 
				return;
			
			var itemPrefab = GetRandomItemConfig().GetItemPrefab();
			Spawn(itemPrefab);

			var randomDestroyTime = UnityEngine.Random.Range(_minSpawnTime, _maxSpawnTime);
			StartCoroutine(DestroySpawnedItemAfter(randomDestroyTime));
		}

        private void InitializeBoxCollider()
        {
            var boxCollider = GetComponent<BoxCollider>();
			Assert.IsNotNull(boxCollider, "A box collider needs to be in the game scene.");

            if (!boxCollider)
            {
                var bc = this.gameObject.AddComponent<BoxCollider>();
                bc.isTrigger = true;
            }
        }

		private IEnumerator DestroySpawnedItemAfter(float delay)
		{
			yield return new WaitForSeconds(delay);
			ClearSpawnedItemFromSpawner();
		}

		private ItemConfig GetRandomItemConfig()
		{
			var r = UnityEngine.Random.Range(0, _itemsToPotentiallySpawn.Count);
			_spawnedItemConfig = _itemsToPotentiallySpawn[r];
			return _itemsToPotentiallySpawn[r];
		}

		private bool ObjectCanSpawn()
        {
            var r = Random.Range(0f, 1f);
            var probability = _probabilityOfSpawnPerSecond * Time.deltaTime;
			return r < probability;
        }
		private void Spawn(GameObject prefab)
        {
            _spawnedItemObject = Instantiate(prefab) as GameObject; ;
            _spawnedItemObject.transform.SetParent(this.transform);
            _spawnedItemObject.transform.localPosition = Vector3.zero;
        }

		void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.GetComponent<Character>()) 
			{
				var character = other.gameObject.GetComponent<Character>();
				AddItemToInventoryOf(character);
				ClearSpawnedItemFromSpawner();
			} 
		}

		private void AddItemToInventoryOf(Character character)
        {
			if (_spawnedItemConfig != null && _spawnedItemConfig.IsWeapon()) 
			{
				var weaponSystem = character.gameObject.GetComponent<WeaponSystem>();
				weaponSystem.UpdateWeapon(_spawnedItemConfig as WeaponConfig);
			} 
        }

        private void ClearSpawnedItemFromSpawner()
        {
            _spawnedItemObject = null;
			_spawnedItemConfig = null;

            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

}

