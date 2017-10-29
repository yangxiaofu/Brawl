using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons{
	public class Projectile : MonoBehaviour {

		[SerializeField] float _timeToDestroy = 2f;

		// Use this for initialization
		void Start () 
		{
			StartCoroutine(DestroyObject(_timeToDestroy));
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent<Enemy>())
			{
				Destroy(this.gameObject);
			}
		}

		IEnumerator DestroyObject(float delay)
		{
			yield return new WaitForSeconds(delay);
			Destroy(this.gameObject);
		}
	}

}
