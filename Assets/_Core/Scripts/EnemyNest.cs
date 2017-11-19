using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Weapons;
using Game.Characters;

namespace Game.Core{
	public class EnemyNest : MonoBehaviour {
		[SerializeField] GameObject _enemyPrefabToSpawn;
		[SerializeField] float _spawnRate;
		EnemyParent _parent;

		void Start()
		{
			StartCoroutine(SpawnEnemy());
			_parent = FindObjectOfType<EnemyParent>();
		}
		IEnumerator SpawnEnemy()
		{
			while (true)
			{
				yield return new WaitForSeconds(_spawnRate);
				var enemyObject = Instantiate(_enemyPrefabToSpawn, this.transform.position, this.transform.rotation);
				enemyObject.transform.SetParent(_parent.transform);
			}
		}
				
		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent(typeof(IProjectile)))
			{
				var projectile = other.gameObject.GetComponent(typeof(IProjectile)) as IProjectile;
				GetComponent<HealthSystem>().DealDamage(projectile.GetDamage());
			}
		}
	}

}

