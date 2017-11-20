using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Weapons;

namespace Game.Core{
	public class Explodeable : MonoBehaviour {

		[Tooltip("The radius of explosion.  Any rigid body within this radius is impacted. ")]
		[SerializeField] float _explosionRadius = 10f;

		[Tooltip("Tweak this to update the amount of hits on a player when hit. ")]
		[SerializeField] float _damageUponExplosion = 50f;

		[Tooltip("This is the amount of force that is applied to surrounding rigidbodies upon explosion.")]
		[SerializeField] float _explosionForce = 100f;

		[Space]
		[Header("Explosion Prefabs")]
		[SerializeField] GameObject _explosionEffectPrefab;
		[SerializeField] AudioClip _explosionAudioClip;

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
                _audioSource.Play();
                StartCoroutine(PlayParticleEffect());
                RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, _explosionRadius, Vector3.up, _explosionRadius);
                DealDamageTo(hits);
				AddForceToDeadHits(hits);
				Destroy(this.gameObject);
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

		private void AddForceToDeadHits(RaycastHit[] hits)
		{
			for(int i = 0; i < hits.Length; i++)
			{
				var direction = (hits[i].collider.gameObject.transform.position - this.transform.position).normalized;
				
				if (hits[i].collider.gameObject.GetComponent<Rigidbody>())
				{
					var rb = hits[i].collider.gameObject.GetComponent<Rigidbody>();
					Assert.IsNotNull(rb, "There is no rigid body on " + hits[i].collider.name);
					rb.AddForce(direction * _explosionForce, ForceMode.Impulse);
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

