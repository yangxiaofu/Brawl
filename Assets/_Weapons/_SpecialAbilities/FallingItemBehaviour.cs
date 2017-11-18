using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Weapons{
	public class FallingItemBehaviour: MonoBehaviour{
        FallingItemsSpecialAbilityConfig _config;

		public void SetupConfig(FallingItemsSpecialAbilityConfig config, Character character)
        {
            _config = config;
        }

        void OnCollisionEnter(Collision other)
		{
            if (other.gameObject == this.gameObject)
                return;

            var randomExplosionTimer = Random.Range(
                _config.minDelayAfterFirstCollision, _config.maxDelayAfterFirstCollision
            );

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

