using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons{
    public class MeleeWeaponConfig : WeaponConfig
    {
        public override BlastConfig GetBlastConfig()
        {
            throw new System.NotImplementedException();
        }

        public override float GetBlastDelayAfterCollision()
        {
            return 0;
        }
    }
}

