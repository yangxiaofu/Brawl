using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Weapons
{
    public class FreezeBlastBehaviour : BlastBehaviour
    {
        List<Character> _frozenCharacter = new List<Character>();
        public override void DoBlastSpecificBehaviour()
        {
            foreach(Character character in _charactersImpactedOnBlast)
			{
                Freeze(character);
                _frozenCharacter.Add(character);
			}
        }

		private void Freeze(Character character)
		{
            character.freeze = true;
            var freezeTime = (_blastConfig as FreezeBlastConfig).freezeTime;
            StartCoroutine(UnFreeze(freezeTime));
		}

        IEnumerator UnFreeze(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            for(int i = 0; i < _frozenCharacter.Count; i++)
            {
                _frozenCharacter[i].freeze = false;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, _blastConfig.blastRadius);
        }
    }
}


