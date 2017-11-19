using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Characters;

namespace Game.Weapons{
	public class WeaponBehaviour : MonoBehaviour {
		protected WeaponConfig _config;
		protected WeaponSystem _weaponSystem;
		protected AudioSource _audioSource;
		
		protected void PlayGunFireAudio()
        {
            _audioSource.clip = _config.GetShootingSound();
            _audioSource.Play();
        }
	}

}
