﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Items;

namespace Game.Characters{
	public class CharacterLogic{
		private readonly Character _character;

		public CharacterLogic(Character character){
			_character = character;
		}

		public bool WillTakeDamageFrom(GameObject gameObject)
		{
			 if (!gameObject.GetComponent<ProjectileBehaviour>())
                return false;

			 var shootingCharacter = gameObject.GetComponent<ProjectileBehaviour>().shootingCharacter;
			 if (shootingCharacter == _character)
                return false;

			return true;
		}
	}
}
