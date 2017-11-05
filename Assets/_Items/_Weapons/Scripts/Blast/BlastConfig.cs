using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	//Blast Configs are the impact upon explosion of the weapon item. 
	public abstract class BlastConfig : ScriptableObject{
		[SerializeField] GameObject _blastImpactParticleSystemPrefab;
		public GameObject GetImpactParticleSystemPrefab(){
			return _blastImpactParticleSystemPrefab;
		}
		[SerializeField] float _blastRadius = 2f;
		public float blastRadius{get{return _blastRadius;}}
		[SerializeField] float _blastForce = 50f;
		public float blastForce{get{return _blastForce;}}
		[SerializeField] float _delayBeforeExplosion = 2f;
		public float delayBeforeExplosion{get{return _delayBeforeExplosion;}}

		public float _damage = 10f;
		public float GetDamage(){return _damage;}
		public abstract BlastBehaviour AddComponentTo(GameObject gameObjectToAddTo);
	}
}


