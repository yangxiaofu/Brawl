using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Weapons
{
	public class PowerWeaponBehaviour : MonoBehaviour 
	{
		PowerWeaponConfig _config;
		float _currentCharge = 0;
		float _maxCharge = 100f;
		PowerWeaponSocket _socket;
		bool _charging = false;
		bool _released = false;
		GameObject _projectileObject;
		
		public void Setup(PowerWeaponConfig config)
		{
			_config = config;

			_socket = GetComponentInChildren<PowerWeaponSocket>();
			Assert.IsNotNull(_socket, "There is no weapon grip transform in the child of " + this.gameObject.name);
		}

		void Update()
		{
			if (_charging)
            {
                ContinueCharge();
            }

            if (_projectileObject && !_released)
			{
				_projectileObject.transform.position = _socket.transform.position;
			}
		}

        private void ContinueCharge()
        {
            _currentCharge += _config.chargePerSecond * Time.deltaTime;
            _currentCharge = Mathf.Clamp(_currentCharge, 0, _maxCharge);
        }

        public void StartCharging()
        {
            _charging = true;
            _released = false;
            var prefab = _config.GetItemPrefab();
            _projectileObject = Instantiate(prefab, _socket.transform.position, Quaternion.identity) as GameObject;
			
            SetupRigidBody();
        }

        private void SetupRigidBody()
        {
            var rb = _projectileObject.GetComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.useGravity = false;
        }

        public void Use()
        {
            if (!_projectileObject) //TODO: The projectile probably has a dstroy Coroutine that shouldn't start until it gets reslease. 
                Debug.LogError("The projectile Object should not be null.");

            SetupProjectile();
            AddForceToProjectile();
            ResetPowerWeaponBehaviour();
        }

        private void SetupProjectile()
        {
			if (_projectileObject == null) 
				Debug.LogError("Projectile Object should not be null.");

			var collider = _projectileObject.AddComponent<SphereCollider>();
            collider.isTrigger = false;



			var behaviour = _config.GetBlastConfig().AddComponentTo(_projectileObject);
			behaviour.Setup(_config, GetComponent<Characters.Character>());
        }

		IEnumerator AddCollider(float delay)
		{
			yield return new WaitForSeconds(delay);

		}

        private void AddForceToProjectile()
        {
			Vector3 forceToAdd = (this.transform.forward * _currentCharge * _config.forceFacter);
            _projectileObject.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);
        }

        private void ResetPowerWeaponBehaviour()
        {
            _released = true;
            _charging = false;
            _currentCharge = 0;
			_projectileObject = null;
        }
    }
}
