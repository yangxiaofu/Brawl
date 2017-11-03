using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Items;
using Game.Core.ControllerInputs;

namespace Game.UI{
	public class PlayerUI : MonoBehaviour 
	{
		[SerializeField] Text _playerNameText;
		[SerializeField] Image _playerAvatarImage;
		[SerializeField] Text _primaryWeaponText;
		[SerializeField] Text _secondaryWeaponText;
		[SerializeField] Text _specialAbilityText;
		[SerializeField] PLAYER_TAG _playerTag;
		Character _character;
		void Start()
        {
			FindPlayerTag();
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            Assert.IsNotNull(_playerNameText);
            _playerNameText.text = _character.name;

            Assert.IsNotNull(_playerAvatarImage);
            Assert.IsNotNull(_primaryWeaponText);
            Assert.IsNotNull(_secondaryWeaponText);
            Assert.IsNotNull(_specialAbilityText);
        }

        void Update()
        {
            UpdatePrimaryAmmoText();
        }

        private void UpdatePrimaryAmmoText()
        {
			var weaponSystem = _character.GetComponent<WeaponSystem>();
			var remainingAmmo = weaponSystem.primaryWeaponBehaviour.remainingAmmo;
			var startingAmmo = weaponSystem.primaryWeaponBehaviour.startingAmmo;
			_primaryWeaponText.text = remainingAmmo + "/" + startingAmmo;
        }

		void FindPlayerTag()
		{
			var controllers = GameObject.FindObjectsOfType<ControllerBehaviour>();	
			for (int i = 0; i < controllers.Length; i++)
			{
				if (controllers[i].playerTag == _playerTag)
				{
					_character = controllers[i].character;
					break;
				}
			}
		}
    }

}
