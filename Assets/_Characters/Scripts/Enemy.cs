using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using Game.Weapons;

namespace Game.Characters
{
	public class Enemy : Character
	{
		
		void Start()
		{
			enemyRenderer = GetComponent<Renderer>();
		}
		void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.GetComponent<Projectile>())
			{
				StartCoroutine(Blink(0.1f, 20));
			}			
		}
	}
}
