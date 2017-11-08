﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	public class FallingItemBehaviour : MonoBehaviour {
		[SerializeField] float _delayAfterFirstCollision = 0f;
		[SerializeField] GameObject _explosionParticleEffectPrefab;

		public void Setup(float delayAfterFirstCollision, GameObject explosionParticleEffect)
		{
			_delayAfterFirstCollision = delayAfterFirstCollision;	
			_explosionParticleEffectPrefab = explosionParticleEffect;
		}

		void OnCollisionEnter(Collision other)
		{
			StartCoroutine(BeginExplosionTimer());
		}
		
		IEnumerator BeginExplosionTimer()
		{
			var randomExplosionTimer = UnityEngine.Random.Range(0, _delayAfterFirstCollision);
			yield return new WaitForSeconds(randomExplosionTimer);
			
			var particleEffectObject = Instantiate(
				_explosionParticleEffectPrefab, 
				this.transform.position, 
				_explosionParticleEffectPrefab.transform.rotation
			);

			var particleSystem = particleEffectObject.GetComponent<ParticleSystem>();

			var duration = particleSystem.main.duration;
			particleSystem.Play();

			Destroy(this.gameObject);
		}

		IEnumerator DestroyParticleSystem(float delay, GameObject particleObject)
		{
			yield return new WaitForSeconds(delay);
			Destroy(particleObject);
		}
	}
}
