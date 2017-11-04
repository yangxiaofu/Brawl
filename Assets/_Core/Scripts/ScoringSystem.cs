using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class ScoringSystem : MonoBehaviour 
	{
		[SerializeField] float _points = 0;
		public float points{get{return _points;}}
		void Start()
		{
			_points = 0;
		}

		public void AddPoints(float pointsToAdd)
		{
			_points += pointsToAdd;
		}
	}
}

