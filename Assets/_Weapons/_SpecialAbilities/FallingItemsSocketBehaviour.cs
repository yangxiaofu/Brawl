using System.Collections;
using System.Collections.Generic;
using Game.Characters;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Weapons{
	public class FallingItemsSocketBehaviour : SocketBehaviour
	{
		List<GameObject> _fallingItemSockets = new List<GameObject>();

        private void GetChildrenSockets()
        {
            foreach (Transform child in transform)
                _fallingItemSockets.Add(child.gameObject);

			Assert.AreNotEqual(0, _fallingItemSockets.Count, "You need to have sockets within teh falling iems prefab.");
        }

        public void BeginFallingItems(GameObject fallingItemsPrefab)
        {
            GetChildrenSockets();
            DropItems(fallingItemsPrefab);
        }

        private void DropItems(GameObject fallingItemsPrefab)
        {
            for (int i = 0; i < _fallingItemSockets.Count; i++)
            {
                var fallingItemObject = Instantiate(
                    fallingItemsPrefab,
                    _fallingItemSockets[i].transform.position,
                    Quaternion.identity
                );
                fallingItemObject.transform.SetParent(_fallingItemSockets[i].transform);
			    var bombBehaviour = fallingItemObject.AddComponent<FallingItemBehaviour>();
                bombBehaviour.SetupConfig((_config as FallingItemsSpecialAbilityConfig), _character);
            }
        }
    }
}

