using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Weapons;

namespace Game.Core{
	public class Explodeable : MonoBehaviour {

		[SerializeField] GameObject _explosionEffectPrefab;
		[SerializeField] AudioClip _explosionAudioClip;
		[SerializeField] float _explosionRadius = 10f;
		[SerializeField] float _damageUponExplosion = 50f;
		AudioSource _audioSource;

		void Start()
		{
			_audioSource = this.gameObject.AddComponent<AudioSource>();
			_audioSource.loop = false;
			_audioSource.playOnAwake = false;
			_audioSource.clip = _explosionAudioClip;

			Assert.IsNotNull(_explosionEffectPrefab);
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent<ProjectileBehaviour>())
            {
                Destroy(this.gameObject);
                _audioSource.Play();
                StartCoroutine(PlayParticleEffect());

                RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, _explosionRadius, Vector3.up, _explosionRadius);

                DealDamageTo(hits);
				AddForceToDead(hits);

            }
        }

        private void DealDamageTo(RaycastHit[] hits)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                var damageable = hits[i].collider.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.DealDamage(_damageUponExplosion);
                }
            }
        }

		private void AddForceToDead(RaycastHit[] hits)
		{
			for(int i = 0; i < hits.Length; i++)
			{
				var direction = (hits[i].collider.gameObject.transform.position - this.transform.position).normalized;
				
				if (hits[i].collider.gameObject.GetComponent<Rigidbody>())
				{
					var rb = hits[i].collider.gameObject.GetComponent<Rigidbody>();
					rb.AddForce(direction * 100f, ForceMode.Impulse);
				}
			}
		}

        IEnumerator PlayParticleEffect()
		{
			var exp = Instantiate(_explosionEffectPrefab, this.transform.position, this.transform.rotation);
			var particleSystem = exp.GetComponent<ParticleSystem>();
			var length = particleSystem.main.duration;

			yield return new WaitForSeconds(length);

			Destroy(this.gameObject);
			
		}
	}
}

