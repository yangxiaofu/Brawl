using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons{

	[CreateAssetMenu(menuName = "Game/Blast/Explosion")]
    public class ExplosionBlastConfig : BlastConfig
    {
        public override BlastBehaviour AddComponentTo(GameObject gameObjectToAddTo)
        {
			return gameObjectToAddTo.AddComponent<ExplosionBlastBehaviour>();
        }
    }

}
