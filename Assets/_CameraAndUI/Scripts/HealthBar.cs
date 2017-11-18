using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Characters;
using Game.Core;
using UnityEngine.Assertions;

namespace Game.CameraUI{
	public class HealthBar : MonoBehaviour {
		[SerializeField] Image _energyBarImage;
		HealthSystem _healthSystem;

		void Start()
		{
			Assert.IsNotNull(_energyBarImage);
			_healthSystem = GetComponentInParent<HealthSystem>();
		}
		void Update()
		{
			_energyBarImage.fillAmount = _healthSystem.healthAsPercentage;
		}
	}

}
