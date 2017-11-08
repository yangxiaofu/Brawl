using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	public class FallingItemBehaviour : MonoBehaviour {
		float _delayAfterFirstCollision = 0f;
		GameObject _explosionParticleEffectPrefab;
		AudioClip _audioClip;
		AudioSource _audioSource;
		public void Setup(float delayAfterFirstCollision, GameObject explosionParticleEffect, AudioClip audioClip)
        {
            _delayAfterFirstCollision = delayAfterFirstCollision;
            _explosionParticleEffectPrefab = explosionParticleEffect;
            _audioClip = audioClip;

            SetupAudioSource(audioClip);
        }

        private void SetupAudioSource(AudioClip audioClip)
        {
            _audioSource = this.gameObject.AddComponent<AudioSource>();
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
            _audioSource.clip = audioClip;
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
			_audioSource.Play();

			Destroy(this.gameObject);
		}

		IEnumerator DestroyParticleSystem(float delay, GameObject particleObject)
		{
			yield return new WaitForSeconds(delay);
			Destroy(particleObject);
		}
	}
}

