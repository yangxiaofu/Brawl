using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons{
	public class BlastBehaviourLogic {
		public bool CanAddForce(bool isEnemy, bool hasRigidBody, bool hasNavMeshAgent, bool isDead){
			if (!isEnemy) 
				return false;

			if (!hasRigidBody)
				return false;

			if (!hasNavMeshAgent)
				return false;

			if (!isDead)
				return false;

			return true;
		}
	}

	
}

