using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Core;

namespace Game.Items{
	public class HealthItemBehaviour : MonoBehaviour {
		[SerializeField] float _healthToIncrease = 50f;

		void Start()
		{
			var bc = GetComponent<BoxCollider>();
			Assert.IsNotNull(bc);
			Assert.IsTrue(bc.isTrigger);
		}

		void OnTriggerEnter(Collider other)
		{
			if (!other.gameObject.GetComponent<MyPlayer>())
				return;
			
			var healthSystem = other.gameObject.GetComponent<HealthSystem>();
			healthSystem.Heal(_healthToIncrease);
			Destroy(this.gameObject);
		}
	}

}
