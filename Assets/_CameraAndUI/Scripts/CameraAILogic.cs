using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CameraUI{

	public class CameraAILogic 
	{
		public Vector3 FindAveragePositionFromGameObjects(List<GameObject> pos)
		{
			Vector3 vectorTotal = Vector3.zero;

			var count = 0;

			for (int i = 0; i < pos.Count; i++)
			{
				vectorTotal += pos[i].transform.position;
				count ++;
			}
			
			return vectorTotal / count;
		}
	}

}
