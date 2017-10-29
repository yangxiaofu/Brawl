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

		IEnumerator DestroyObject(float delay)
		{
			yield return new WaitForSeconds(delay);
			Destroy(this.gameObject);
		}
	}

}
