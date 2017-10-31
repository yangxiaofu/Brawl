using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Items;

namespace Game.UI{
	public class PlayerUI : MonoBehaviour 
	{
		[SerializeField] string _playerTag;
		[SerializeField] Text _playerNameText;
		[SerializeField] Image _playerAvatarImage;
		[SerializeField] Text _primaryWeaponText;
		[SerializeField] Text _secondaryWeaponText;
		[SerializeField] Text _specialAbilityText;
		GameObject _character;
		void Start()
		{
			_character = GameObject.FindGameObjectWithTag(_playerTag);
			Assert.IsNotNull(_character, "You are missing a player tagged " + _playerTag + " in the game scene.");
			
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
    }

}
