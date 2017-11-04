using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class ScoreSettings : MonoBehaviour {
		[SerializeField] float _pointsPerHit = 10f;
		public float GetPointsPerHit(){return _pointsPerHit;}
	}
}

