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
		[SerializeField] Text _playerNameText;
		[SerializeField] Image _playerAvatarImage;
		[SerializeField] Text _primaryWeaponText;
		[SerializeField] Text _secondaryWeaponText;
		[SerializeField] Text _specialAbilityText;
		[SerializeField] Text _scoringSystemText;
		[SerializeField] PLAYER_TAG _playerTag;
		EnergyBar _energyBar;
		Character _character;
		ScoringSystem _scoringSystem;
		ControllerBehaviour _controller;

		void Start()
        {
            SetupCharacter();
            SetupScoringSystem();
            InitializeVariables();
            SetupEnergyBar();
        }

        void Update()
        {
            UpdatePrimaryAmmoText();
			UpdateCharacterScore();
        }

        private void SetupEnergyBar()
        {
            _energyBar = gameObject.GetComponentInChildren<EnergyBar>();
            Assert.IsNotNull(_energyBar);
            _energyBar.SetupCharacter(_character);
        }

        private void SetupScoringSystem()
        {
            _scoringSystem = _controller.GetComponent<ScoringSystem>();
            Assert.IsNotNull(_scoringSystem, "You need to add a scoring system component to the character " + _controller.name);
        }

        private void InitializeVariables()
        {
            Assert.IsNotNull(_playerNameText);
            _playerNameText.text = _character.name;

            Assert.IsNotNull(_playerAvatarImage);
            Assert.IsNotNull(_primaryWeaponText);
            Assert.IsNotNull(_secondaryWeaponText);
            Assert.IsNotNull(_specialAbilityText);
			Assert.IsNotNull(_scoringSystemText);
        }
        private void UpdateCharacterScore()
        {
            _scoringSystemText.text = _scoringSystem.points.ToString();
        }

        private void UpdatePrimaryAmmoText()
        {
			var weaponSystem = _character.GetComponent<WeaponSystem>();
			var remainingAmmo = weaponSystem.primaryWeaponBehaviour.remainingAmmo;
			var startingAmmo = weaponSystem.primaryWeaponBehaviour.startingAmmo;
			_primaryWeaponText.text = remainingAmmo + "/" + startingAmmo;
        }

		void SetupCharacter()
		{
			var controllers = GameObject.FindObjectsOfType<ControllerBehaviour>();	
			for (int i = 0; i < controllers.Length; i++)
			{
				if (controllers[i].playerTag == _playerTag)
				{
					_character = controllers[i].character;
					_controller = controllers[i];
					break;
				}
			}
		}
    }

}
