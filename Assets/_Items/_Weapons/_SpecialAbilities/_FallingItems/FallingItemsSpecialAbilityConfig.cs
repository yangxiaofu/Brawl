using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core;

namespace Game.Items{
	[CreateAssetMenu(menuName = "Game/Special Ability/Falling Items")]
	public class FallingItemsSpecialAbilityConfig : SpecialAbilityConfig
	{
		[Header("Falling Bombs Specific")]
		[SerializeField] GameObject _fallingItemsSocketPrefab;
		[SerializeField] float _delayExplosionAfterFirstCollision = 0f;
		public float delayExplosionAfterFirstCollision{get{return _delayExplosionAfterFirstCollision;}}
		[SerializeField] GameObject _fallingObjectPrefab;
		[SerializeField] GameObject _explosionParticleEffectPrefab;
		public GameObject explosionParticleEffectPrefab{get{return _explosionParticleEffectPrefab;}}
        public override void Use()
        {
            var socketObject = FindObjectOfType<SkySpecialAbilitySocket>();
            Assert.IsNotNull(socketObject, "You are missing the SkySpecialAbilitySocket in the game scene.");

            var fallingItemsSocketObject = Instantiate(
                _fallingItemsSocketPrefab,
                socketObject.transform.position,
                Quaternion.Euler(0, Random.Range(0, 360), 0)
            ) as GameObject;

            var behaviour = fallingItemsSocketObject.AddComponent<FallingItemsBehaviour>();
            behaviour.SetupConfig(this);
            behaviour.BeginFallingItems(_fallingObjectPrefab);
        }
    }
}

