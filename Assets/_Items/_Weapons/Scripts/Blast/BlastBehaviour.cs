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
        protected List<Character> _charactersImpactedOnBlast = new List<Character>();
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
			if (other.gameObject.GetComponent<Character>() == _character)
				return;

			StartCoroutine(StartExplosionTimer(_weaponConfig.GetBlastDelayAfterCollision()));
		}

		IEnumerator StartExplosionTimer(float delay)
        {
            yield return new WaitForSeconds(delay);

            UpdateScore();

            GameObject particleSystemObject = HandleParticleSystem();

            Destroy(this.gameObject);
            
        }

        private void UpdateScore()
        {
            float totalPoints = CalculateTotalPoints();
            var scoringSystem = _character.controller.GetComponent<ScoringSystem>();
            scoringSystem.AddPoints(totalPoints);
        }

        private GameObject HandleParticleSystem()
        {
            var particleSystemPrefab = _blastConfig.GetImpactParticleSystemPrefab();
            var particleSystemObject = Instantiate(particleSystemPrefab, this.transform.position, _blastConfig.GetImpactParticleSystemPrefab().transform.rotation) as GameObject;
            var particleSystem = particleSystemObject.GetComponent<ParticleSystem>();
            particleSystem.Play();

            var destroyTimer = particleSystemObject.AddComponent<DestroyTimer>();
            destroyTimer.SetupTimer(particleSystem.main.duration);
            destroyTimer.Begin();

            return particleSystemObject;
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

		private int PerformBlast()
        {
            var totalHits = 0;
            var hits = Physics.SphereCastAll(this.transform.position, _blastConfig.blastRadius, Vector3.up);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<Character>())
                {
                    var hitCharacter = hit.collider.gameObject.GetComponent<Character>();
                    _charactersImpactedOnBlast.Add(hitCharacter);

                    if (hitCharacter != _character)
                    {
						totalHits += 1;
                    }
                }
            }

            DoBlastSpecificBehaviour();

            return totalHits;
        }

        public abstract void DoBlastSpecificBehaviour();
	}
}

