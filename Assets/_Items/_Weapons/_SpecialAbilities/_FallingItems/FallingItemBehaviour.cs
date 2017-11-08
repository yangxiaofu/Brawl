using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	public class FallingItemBehaviour : MonoBehaviour {
		float _delayAfterFirstCollision = 0f;
		GameObject _explosionParticleEffectPrefab;
		AudioClip _audioClip;

		public void Setup(float delayAfterFirstCollision, GameObject explosionParticleEffect, AudioClip audioClip)
        {
            _delayAfterFirstCollision = delayAfterFirstCollision;
            _explosionParticleEffectPrefab = explosionParticleEffect;
            _audioClip = audioClip;
        }

        void OnCollisionEnter(Collision other)
		{
			StartCoroutine(BeginExplosionTimer());
		}
		
		IEnumerator BeginExplosionTimer()
        {
            var randomExplosionTimer = UnityEngine.Random.Range(0, _delayAfterFirstCollision);

            yield return new WaitForSeconds(randomExplosionTimer);

            var particleEffectObject = Instantiate(_explosionParticleEffectPrefab, this.transform.position, _explosionParticleEffectPrefab.transform.rotation);

            var particleSystem = particleEffectObject.GetComponent<ParticleSystem>();
            particleSystem.Play();
			
            PlayExplosionAudioOn(particleEffectObject);

            Destroy(this.gameObject);
        }

        private void PlayExplosionAudioOn(GameObject particleEffectObject)
        {
            var audioSource = particleEffectObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            audioSource.clip = _audioClip;
            particleEffectObject.GetComponent<AudioSource>().Play();
        }
    }
}

