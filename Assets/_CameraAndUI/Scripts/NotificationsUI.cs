using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace Game.UI{
	public class NotificationsUI : MonoBehaviour {
		[SerializeField] Text _text;
		void Awake()
		{
			Assert.IsNotNull(_text);
		}
	}

}
