﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Core;

namespace Game.Weapons{
	public abstract class BlastBehaviour : MonoBehaviour 
	{
		protected Character _character;
		protected BlastConfig _blastConfig;
        protected WeaponConfig _weaponConfig;
        protected List<Character> _charactersImpactedOnBlast = new List<Character>();
        protected List<GameObject> _objectsImpactedOnBlast = new List<GameObject>();

        protected float _blastForce;
		public void Setup(WeaponConfig weaponConfig, Character character)
        {
			_character = character;
			_blastConfig = weaponConfig.GetBlastConfig();
            _blastForce = _blastConfig.blastForce;
            _weaponConfig = weaponConfig;
		}

        public void UpdateBlastForce(float totalBlastForce)
        {
            _blastForce = totalBlastForce;
        }

        public float GetDamage()
        {
            return _blastConfig.GetDamage();
        }

		void OnCollisionEnter(Collision other)
		{
            var character = other.gameObject.GetComponentInParent<Character>();

            if (character && character == _character)
                return;

			if (other.gameObject.GetComponent<Character>() == _character)
				return;
                
			StartCoroutine(StartExplosionTimer(_weaponConfig.GetBlastDelayAfterCollision()));
		}

		IEnumerator StartExplosionTimer(float delay)
        {
            yield return new WaitForSeconds(delay);

            UpdateScore();

            HandleParticleSystem();
        }

        private void UpdateScore()
        {
            float totalPoints = CalculateTotalPoints();
            var scoringSystem = _character.controller.GetComponent<ScoringSystem>();
            scoringSystem.AddPoints(totalPoints);
        }

        private void HandleParticleSystem() //TODO: Refactor out to some form of delegation.  The other similar script is in FallingItemBehaviour
        {
            //Instantiate the object.
            var blastObject = Instantiate(_blastConfig.GetBlastPrefab(), this.transform.position, _blastConfig.GetBlastPrefab().transform.rotation) as GameObject;
            var blastLength = blastObject.GetComponent<ExplosionBehaviour>().GetBlastDuration();
            PlayExplosionAudioOn(blastObject);

            var destroyTimer = blastObject.AddComponent<DestroyTimer>();
            destroyTimer.SetupTimer(blastLength); 
            destroyTimer.Begin();

            //DestroyGameObject
            Destroy(this.gameObject);
        }

        private void PlayExplosionAudioOn(GameObject particleSystemObject)
        {
            var audioSource = particleSystemObject.AddComponent<AudioSource>();
            audioSource.volume = GameManager.Instance().GetAudioVolume();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            audioSource.clip = _weaponConfig.GetBlastConfig().GetAudio();
            audioSource.Play();
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
                var hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Character>())
                {
                    var hitCharacter = hit.collider.gameObject.GetComponent<Character>();
                    _charactersImpactedOnBlast.Add(hitCharacter);

                    if (hitCharacter != _character)
                    {
						totalHits += 1;
                    }
                }
                else 
                {
                    _objectsImpactedOnBlast.Add(hitObject);
                }
            }

            DoBlastSpecificBehaviour();

            return totalHits;
        }

        protected void ApplyBlastForceTo(GameObject characterObject)
        {
            if (characterObject.GetComponent<Rigidbody>())
            {
                var forceDirection = (characterObject.transform.position - this.transform.position).normalized;
                var newDirection = new Vector3(forceDirection.x, 1, forceDirection.z);
                var rigidBody = characterObject.gameObject.GetComponent<Rigidbody>();                 
                rigidBody.AddForce(newDirection * _blastForce, ForceMode.Impulse);
            }   
        }

        public abstract void DoBlastSpecificBehaviour();
	}
}
