using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Weapons;
using Game.Characters;

namespace Game.Core{
	public class EnemyNest : MonoBehaviour {
		[SerializeField] GameObject _enemyPrefabToSpawn;
		[SerializeField] float _spawnRate;

		void Start()
		{
			StartCoroutine(SpawnEnemy());
		}
		IEnumerator SpawnEnemy()
		{
			while (true)
			{
				yield return new WaitForSeconds(_spawnRate);
				var enemyObject = Instantiate(_enemyPrefabToSpawn, this.transform.position, this.transform.rotation);
			}
		}
				
		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent(typeof(IDestructable)))
			{
				var d = other.gameObject.GetComponent(typeof(IDestructable)) as IDestructable;
				GetComponent<HealthSystem>().DealDamage(d.GetDamage());
			}
		}
	}

}

