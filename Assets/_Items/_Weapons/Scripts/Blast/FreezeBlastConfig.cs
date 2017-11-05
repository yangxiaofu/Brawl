using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Items{
    [CreateAssetMenu(menuName = "Game/Blast/Freeze")]
    public class FreezeBlastConfig : BlastConfig
    {
        [SerializeField] float _freezeTime = 5f;
        public float freezeTime{get{return _freezeTime;}}
        public override BlastBehaviour AddComponentTo(GameObject gameObjectToAddTo)
        {
            return gameObjectToAddTo.AddComponent<FreezeBlastBehaviour>();
        }
    }
}

