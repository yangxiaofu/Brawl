using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	
	public class HealthSystem : MonoBehaviour {
		[SerializeField] float _healthImprovedPerSecond = 5f;
		[SerializeField] int _numberOfTimesHitBeforeDestroying = 10;
		[SerializeField] float _scaleUpIncrementsWhenHit = 0.1f;
		int _timesHit = 0;
		HealthSystemLogic _healthSystemLogic;

		void Start()
		{
			_healthSystemLogic = new HealthSystemLogic();
		}

		void Update()
		{
			var healthToAdd = _healthImprovedPerSecond * Time.deltaTime;
		}

		public void TakeHit()
		{
			_timesHit += 1;

			// if (_timesHit >= _numberOfTimesHitBeforeDestroying) //TODO: Commented out for debugging purpposes. 
			// 	Destroy(this.gameObject);

			this.gameObject.transform.localScale = _healthSystemLogic.GrowOpponent(new Vector3(1, 1, 1), _timesHit, _scaleUpIncrementsWhenHit);
		}
	}
}

