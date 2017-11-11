using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Core{
	public class GameManager : MonoBehaviour { //Used as a placeholder. 
		private static GameManager _instance;
		public static GameManager Instance(){
			if (!_instance){
				_instance = FindObjectOfType<GameManager>();
				if (!_instance) Debug.LogError("There is no Game Manager script on the game manager");
			}

			return _instance;
		}
		

		[Tooltip("This will adjust the volume of the blasts in the game.")]
		[Range(0, 1)]
		[SerializeField] float _audioVolume = 0.5f;
		public float GetAudioVolume(){return _audioVolume;}


		ScoreSettings _scoreSettings;
		public ScoreSettings scoreSettings{get{return _scoreSettings;}}

		void Start()
		{
			_scoreSettings = GetComponent<ScoreSettings>();
			Assert.IsNotNull(_scoreSettings, "You need a ScoreSettings script on the game manager " + gameObject.name);
		}
	}
}

