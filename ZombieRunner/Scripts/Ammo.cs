using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieFPS.Ammo
{
    public class Ammo : MonoBehaviour
    {
        AmmoSlot[] ammoSlots = new AmmoSlot[] { new AmmoSlot(AmmoType.Bullets, 10), new AmmoSlot(AmmoType.Rockets, 10), new AmmoSlot(AmmoType.Shells, 10) };

        [System.Serializable]
        private class AmmoSlot
        {
            public AmmoSlot(AmmoType type, int amount)
            {
                ammoType = type;
                ammoAmount = amount;
            }
            public AmmoType ammoType;
            public int ammoAmount;
        }

        public int GetCurrentAmmo(AmmoType ammoType)
        {
            return GetAmmoSlot(ammoType).ammoAmount;
        }

        public void ReduceCurrentAmmo(AmmoType ammoType)
        {
            GetAmmoSlot(ammoType).ammoAmount--;
        }

        public void IncreaseCurrentAmmo(AmmoType ammoType, int amount)
        {
            GetAmmoSlot(ammoType).ammoAmount += amount;
        }

        private AmmoSlot GetAmmoSlot(AmmoType ammoType)
        {
            foreach (AmmoSlot slot in ammoSlots)
            {
                if (slot.ammoType == ammoType)
                {
                    return slot;
                }
            }
            return null;
        }


    }
}