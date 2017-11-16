﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons{
	//Blast Configs are the impact upon explosion of the weapon item. 
	public abstract class BlastConfig : ScriptableObject{
		[SerializeField] GameObject _blastPrefab;
		public GameObject GetBlastPrefab(){return _blastPrefab;}
		[SerializeField] float _blastRadius = 2f;
		public float blastRadius{get{return _blastRadius;}}
		[SerializeField] float _blastForce = 50f;
		public float blastForce{get{return _blastForce;}}
		[SerializeField] float _delayBeforeExplosion = 2f;
		public float delayBeforeExplosion{get{return _delayBeforeExplosion;}}
		[SerializeField] float _damage = 10f;
		public float GetDamage(){return _damage;}
		[SerializeField] AudioClip _audio;
		public AudioClip GetAudio(){ return _audio;}
		public abstract BlastBehaviour AddComponentTo(GameObject gameObjectToAddTo);
	}
}

