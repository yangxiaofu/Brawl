using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Panda;
using Game.Weapons;

namespace Game.Characters
{
	public class Enemy : Character
	{

        void Start()
		{
			_characterRenderer = GetComponentInChildren<Renderer>();
			Assert.IsNotNull(_characterRenderer);
		}

		public override void OnCollisionEnterAction(Collision other)
        {
            //TODO: Do Ragdoll effect on collision.
        }
		
	}
}
