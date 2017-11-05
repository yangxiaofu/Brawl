using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class TotalPointsCalculator : ICalculate
    {
		float _totalHits = 0;
		float _pointsPerHit = 0;
		
		public TotalPointsCalculator(float totalHits, float pointsPerHit)
		{
			_totalHits = totalHits;
			_pointsPerHit = pointsPerHit;
		}

        public float Calculate()
        {
            var totalPoints = _totalHits * _pointsPerHit;
            return totalPoints;
        }
    }
}

