using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
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

