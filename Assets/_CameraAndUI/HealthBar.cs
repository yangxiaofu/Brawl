using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.UI
{
	public class HealthBar : MonoBehaviour 
	{
		[SerializeField] Image _healthBarImage;
		Character _character;

		void Start()
		{
			Assert.IsNotNull(_healthBarImage);
		}

		public void SetupCharacter(Character character)
		{
			_character = character;
		}

		void Update()
		{
			_healthBarImage.fillAmount = _character
				.GetComponent<HealthSystem>().healthAsPercentage;
		}
	}
}

