using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	
	public class HealthSystem : MonoBehaviour 
	{
		[Tooltip("This is the number of times the player gets hit before the player will get destroyed.")]
		[SerializeField] int _numberOfTimesHitBeforeDestroying = 10;

		[Tooltip("Each time the player is hit, the player will scale depending on the factor provided here.")]
		[SerializeField] float _scaleUpIncrementsWhenHit = 0.1f;
		int _timesHit = 0;
		HealthSystemLogic _healthSystemLogic;

		void Start()
		{
			_healthSystemLogic = new HealthSystemLogic();
		}

		public void TakeHit()
		{
			_timesHit += 1;
			
			//TODO: Remove once debugging is complete. 
			// if (_timesHit >= _numberOfTimesHitBeforeDestroying) //TODO: Commented out for debugging purpposes. 
			// 	Destroy(this.gameObject);

			this.gameObject.transform.localScale = _healthSystemLogic.GrowOpponent(new Vector3(1, 1, 1), _timesHit, _scaleUpIncrementsWhenHit);
		}
	}
}

