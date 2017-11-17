using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Weapons;

namespace Game.Core{
	public class Destructable : MonoBehaviour {
		[SerializeField] GameObject _impactEffect;

		bool _hit = false;

		void Start()
		{
			Assert.IsNotNull(_impactEffect);
		}
		
		void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.GetComponent(typeof(IDestructable)) && !_hit){
				_hit = true;
				Destruct();	
			}
		}

		private void Destruct()
		{
			var impactEffectObject = Instantiate(_impactEffect, this.transform.position, this.transform.rotation);
		}
	}
}

