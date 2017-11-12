using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core;

namespace Game.Weapons{
	[CreateAssetMenu(menuName = "Game/Special Ability/Falling Items")]
	public class FallingItemsSpecialAbilityConfig : SpecialAbilityConfig
	{
		[Header("Falling Bombs Specific")]
		[SerializeField] GameObject _fallingItemsSocketPrefab;
        [SerializeField] float _minDelayAfterFirstExplosion = 1f;
        public float minDelayAfterFirstCollision{get{return _minDelayAfterFirstExplosion;}}
		[SerializeField] float _maxDelayAfterFirstCollision = 3f;
		public float maxDelayAfterFirstCollision{get{return _maxDelayAfterFirstCollision;}}
		[SerializeField] GameObject _fallingObjectPrefab;
        [SerializeField] BlastConfig _blastConfig;
        public BlastConfig blastConfig{get{return _blastConfig;}}
        public override GameObject SetupSocket()
        {
            var socketObject = FindObjectOfType<SkySpecialAbilitySocket>().gameObject;

            var fallingItemsSocketObject = Instantiate(
                _fallingItemsSocketPrefab,
                socketObject.transform.position,
                Quaternion.Euler(0, Random.Range(0, 360), 0)
            ) as GameObject;
            
            return fallingItemsSocketObject;
        }

        public override SocketBehaviour AddComponentTo(GameObject gameObjectToAddTo)
        {
            return gameObjectToAddTo.AddComponent<FallingItemsSocketBehaviour>();
        }

        public override void Use(SocketBehaviour behaviour)
        {  
            (behaviour as FallingItemsSocketBehaviour).BeginFallingItems(_fallingObjectPrefab);
        }
    }
}

