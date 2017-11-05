using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;

namespace Game.Items{
	public abstract class BlastBehaviour : MonoBehaviour 
	{
		protected Character _character;
		protected BlastConfig _blastConfig;
        protected WeaponConfig _weaponConfig;
		public void Setup(WeaponConfig weaponConfig, Character character){
			_character = character;
			_blastConfig = weaponConfig.GetBlastConfig();
            _weaponConfig = weaponConfig;
		}

        public float GetDamage()
        {
            return _blastConfig.GetDamage();
        }

		void OnCollisionEnter(Collision other)
		{
			//Start the timer for exploseion.
			if (other.gameObject.GetComponent<Character>() == _character)
				return;

			StartCoroutine(StartExplosionTimer(_weaponConfig.GetBlastDelayAfterCollision()));
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

		private void PlayParticleSystem()
        {
            var particleSystemPrefab = _blastConfig.GetImpactParticleSystemPrefab();

            var particleSystemObject = Instantiate(
                particleSystemPrefab,
                this.transform.position,
                _blastConfig.GetImpactParticleSystemPrefab().transform.rotation
            ) as GameObject;

            var particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
            particleSystem.Play();

			StartCoroutine(DestroyParticleSystem(particleSystem.main.duration));            
        }

		private float CalculateTotalPoints()
        {
            var pointsPerHit = GameManager.Instance().scoreSettings.GetPointsPerHit();
			var totalHits = PerformBlast();
            var calculator = new Calculator(
                new TotalPointsCalculator(totalHits, pointsPerHit)
            );
            var totalPoints = calculator.Calculate();
            return totalPoints;
        }

		protected void ApplyForce(GameObject characterObject)
        {
             var forceDirection = (characterObject.transform.position - this.transform.position).normalized;
             var force = _blastConfig.blastForce;
             characterObject.GetComponent<Rigidbody>().AddForce(forceDirection * force, ForceMode.Impulse);
        }

		IEnumerator DestroyParticleSystem(float delay){
			yield return new WaitForSeconds(delay);
			Destroy(this.gameObject);
		}

		public abstract int PerformBlast();
	}
}

