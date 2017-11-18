using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Weapons;
using Game.Core;
using Game.Core.ControllerInputs;
using System;

namespace Game.UI{
	public class PlayerUI : MonoBehaviour 
	{
		[SerializeField] Text _ammoText;
		[SerializeField] ControllerBehaviour _controller;
		MyPlayer _player;
		WeaponSystem _weaponSystem;
		void Start()
		{
			_player = _controller.character as MyPlayer;
			_weaponSystem = _player.GetComponent<WeaponSystem>();
		}
		
		void Update()
		{
			_ammoText.text = _weaponSystem.primaryWeaponBehaviour.remainingAmmo.ToString();
		}
    }
}
