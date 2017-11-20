using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Weapons;

namespace Game.Core{
	public class HittableObjects : Hittable, IHittable
    {
		List<RaycastHit> _objectsToHit = new List<RaycastHit>();
		public HittableObjects(HittableArgs args)
		{
			_hits = args.hits;
			_explosionPosition = args.explosionPosition;
			_shootingCharacter = args.shootingCharacter;
			_weaponConfig = args.weaponConfig;
		}

        public void ApplyForce()
        {            
            //Compile everything except for the characters.
            FilterOutCharacters();
            //Hits all other objects but the characters.
            ApplyForceToFilteredObjects();
        }

        private void ApplyForceToFilteredObjects()
        {
            for (int j = 0; j < _objectsToHit.Count; j++)
            {
                var hitObject = _objectsToHit[j].collider.gameObject;

                float verticleForceFactor = 0.5f;

                var direction = (hitObject.transform.position - _explosionPosition).normalized;
                var forceDirection = new Vector3(direction.x, verticleForceFactor, direction.z);

                var rb = hitObject.GetComponent<Rigidbody>();

                if (rb)
                {
                    rb.AddForce(forceDirection * (_weaponConfig as PowerWeaponConfig).explosionForceFactor, ForceMode.Impulse);
                }
            }
        }

        private void FilterOutCharacters()
        {
            for (int i = 0; i < _hits.Length; i++)
            {
                var hitCharacter = _hits[i].collider.gameObject.GetComponent<Character>();
                if (!hitCharacter)
                {
                    _objectsToHit.Add(_hits[i]);
                }
            }
        }
    }
}

