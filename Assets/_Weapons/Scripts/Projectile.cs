using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Weapons{
	public class Projectile : MonoBehaviour {

		[SerializeField] float _timeToDestroy = 2f;

		void Start () 
		{
			StartCoroutine(DestroyObjectAfter(_timeToDestroy));
		}

		public void SetTimeBeforeDestroying(float seconds)
		{
			StopAllCoroutines();
			StartCoroutine(DestroyObjectAfter(seconds));
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent<Enemy>())
			{
				Destroy(this.gameObject);
			}
		}

		IEnumerator DestroyObjectAfter(float delay)
		{
			yield return new WaitForSeconds(delay);
			Destroy(this.gameObject);
		}
	}

}
