using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class DestroyTimer : MonoBehaviour {

		private float _delay = 0;
		public void SetupTimer(float delay)
		{
			_delay = delay;
		}

		///<summary>No adding the Being timer to this means that the timer is set to zero by default.!-- </summary>
		public void Begin()
		{
			StartCoroutine(BeginTimer());
		}

		private IEnumerator BeginTimer()
		{
			yield return new WaitForSeconds(_delay);
			Destroy(this.gameObject);
		}
	}
}

