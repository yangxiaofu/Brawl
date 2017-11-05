using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class ScoringSystem : MonoBehaviour 
	{
		private float _points = 0;
		public float points{ get{return _points;}}

		public void AddPoints(float pointsToAdd)
		{
			_points += pointsToAdd;
		}
	}
}

