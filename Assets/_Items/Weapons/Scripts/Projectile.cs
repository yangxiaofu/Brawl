using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items{
	public class Projectile : MonoBehaviour {

		[SerializeField] float _timeToDestroy = 2f;
		[SerializeField] float _damagePerHit = 10f;
		public float damagePerHit{get{return _damagePerHit;}}

		void Start () 
		{
			StartCoroutine(DestroyObjectAfter(_timeToDestroy));
		}

		public void SetDamage(float damage)
		{
			_damagePerHit = damage;
		}


		public void SetTimeBeforeDestroying(float seconds)
		{
			StopAllCoroutines();
			StartCoroutine(DestroyObjectAfter(seconds));
		}

		void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.GetComponent<Enemy>())
				Destroy(this.gameObject);
		}

		IEnumerator DestroyObjectAfter(float delay)
		{
			yield return new WaitForSeconds(delay);
			Destroy(this.gameObject);
		}
	}

}
