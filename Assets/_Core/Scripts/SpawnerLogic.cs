using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
	public class SpawnerLogic 
	{
		public bool ContainsSpawnedObject(GameObject myGameObject)
		{
			return myGameObject.transform.childCount > 0;
		}

	}
}

