using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.Core
{
	public class Kickable : MonoBehaviour {
        [SerializeField] float _forceOnContact = 100f;
        [SerializeField] AudioClip _audioOnImpact;
        Vector3 _forceDirection = Vector3.zero;
		KickableLogic _logic;
        AudioSource _audioSource;
        bool _inMotion = false;

        void Awake()
        {
            _audioSource = this.gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
            _audioSource.clip = _audioOnImpact;
            
            Assert.IsNotNull(_audioOnImpact, "You must add audio for the impact sound in " + this.name);
        }

        void Start()
        {
            _logic = new KickableLogic();
        }

        public void Setup(Vector3 forceDirection)
        {
            _forceDirection = forceDirection;
            _inMotion = true;
        }
		
		void OnCollisionEnter(Collision other)
        {
            if(_logic.ForceCanBeAdded(_inMotion, IsMoveableObject(other)))
            {
                PlayImpactAudio();
                AddForceTo(other.collider.gameObject);
            }
            
            _inMotion = false;
        }

        private void PlayImpactAudio()
        {
            _audioSource.Play();
        }

        private bool IsMoveableObject(Collision other)
        {
            //Can be updated with other parameters later. 
            return other.collider.gameObject.GetComponent<Character>();
        }

        public void AddForceTo(GameObject gameObjectToAddForceTo)
		{
            var rb = gameObjectToAddForceTo.GetComponent<Rigidbody>();
            if (!rb) Debug.LogError("There is no rigid body on this character.  You should consider adding a rigid body to the character in order to realizeForce.");

            rb.AddForce(_forceDirection * _forceOnContact, ForceMode.Impulse);
            Debug.Log("Added Force to the " + gameObjectToAddForceTo.name);

            //Add some sort of sound .ater. 
            
		}


	}
}

