using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items{
	public class FallingItemBehaviour: MonoBehaviour{
        FallingItemsSpecialAbilityConfig _config;
        Character _character;

		public void SetupConfig(FallingItemsSpecialAbilityConfig config, Character character)
        {
            _config = config;
            _character = character;
        }

        void OnCollisionEnter(Collision other)
		{
            if (other.gameObject == this.gameObject)
                return;

            var randomExplosionTimer = Random.Range(
                _config.minDelayAfterFirstCollision, _config.maxDelayAfterFirstCollision
            );

			StartCoroutine(StartExplosionTimer(randomExplosionTimer));
		}
		
		IEnumerator StartExplosionTimer(float delay)
        {
            yield return new WaitForSeconds(delay);

            HandleParticleSystem();
        }

        private void HandleParticleSystem() //TODO: Refactor out to some form of delegation.  The other similar script is in BlastBehaviour
        {
            var particlePrefab = _config.blastConfig.GetBlastPrefab();

            var particleEffectObject = Instantiate(
                particlePrefab, 
                this.transform.position, 
                particlePrefab.transform.rotation
            );

            var particleSystem = particleEffectObject.GetComponentInChildren<Animator>();
            
            PlayExplosionAudioOn(particleEffectObject);
            
            Destroy(this.gameObject);
        }

        private void PlayExplosionAudioOn(GameObject particleEffectObject)
        {
            var audioSource = particleEffectObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            audioSource.clip = _config.GetAudioClip();
            audioSource.Play();
        }
    }
}

