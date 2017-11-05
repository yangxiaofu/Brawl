using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;

namespace Game.Items{
	public class ThrowableWeaponBehaviour : MonoBehaviour {
		Character _character;
		ThrowableWeaponConfig _config;

		public void Setup(ThrowableWeaponConfig config, Character character){
			_character = character;
			_config = config;
		}
		
		void OnCollisionEnter(Collision other)
		{
			//Start the timer for exploseion.
			if (other.gameObject.GetComponent<Character>() == _character)
				return;

			StartCoroutine(StartExplosionTimer(_config.blastDelayAfterCollision));
		}

		IEnumerator StartExplosionTimer(float delay)
        {
            yield return new WaitForSeconds(delay);
            PlayParticleSystem();

            float totalPoints = CalculateTotalPoints();
            var scoringSystem = _character.controller.GetComponent<ScoringSystem>();
            scoringSystem.AddPoints(totalPoints);

            Destroy(this.gameObject);
        }

        private float CalculateTotalPoints()
        {
            var pointsPerHit = GameManager.Instance().scoreSettings.GetPointsPerHit();
            var calculator = new Calculator(
                new TotalPointsCalculator(SphereCastAll(), pointsPerHit)
            );
            var totalPoints = calculator.Calculate();
            return totalPoints;
        }

        private int SphereCastAll()
        {
			var totalHits = 0;
            var hits = Physics.SphereCastAll(this.transform.position, _config.blastRadius, Vector3.up);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<Character>())
                {
                    var hitCharacter = hit.collider.gameObject.GetComponent<Character>();

                    if (hitCharacter != _character)
                    {
						totalHits += 1;
                        
                    }
                }
            }

            return totalHits;
        }

        private void PlayParticleSystem()
        {
            var particleSystemPrefab = _config.GetParticleSystemPrefab();

            var particleSystemObject = Instantiate(
                particleSystemPrefab,
                this.transform.position,
                _config.GetParticleSystemPrefab().transform.rotation
            ) as GameObject;

            var particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
            particleSystem.Play();

			StartCoroutine(DestroyParticleSystem(particleSystem.main.duration));
            
        }

        IEnumerator DestroyParticleSystem(float delay){
			yield return new WaitForSeconds(delay);
			Destroy(this.gameObject);
		}
	}

}
