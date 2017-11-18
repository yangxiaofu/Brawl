using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.CameraUI{
	public class EnergyBar : MonoBehaviour 
	{
		[SerializeField] Image _energyBarImage;
		Character _character;

		void Start()
		{
			Assert.IsNotNull(_energyBarImage);

			_character = GetComponentInParent<Character>();
		}
		void Update()
		{
			_energyBarImage.fillAmount = _character
				.GetComponent<EnergySystem>().energyAsPercentage;
		}
	}
}

