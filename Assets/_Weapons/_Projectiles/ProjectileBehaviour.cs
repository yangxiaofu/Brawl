using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Core;

namespace Game.Weapons{
	public class ProjectileBehaviour : MonoBehaviour, IDestructable
	{
		[HideInInspector] public Vector3 travelDirection = Vector3.zero;
		[HideInInspector] public Character shootingCharacter;

	}
}
