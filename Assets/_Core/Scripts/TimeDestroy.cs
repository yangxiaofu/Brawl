using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class TimeDestroy : MonoBehaviour {

		public void DestroyIn(float seconds)
		{
			StartCoroutine(DestroySelf(seconds));
		}
		
		private IEnumerator DestroySelf(float seconds)
		{
			yield return new WaitForSeconds(seconds);
			Destroy(this.gameObject);
		}		
	}
}

