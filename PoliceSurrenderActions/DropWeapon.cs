using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceSurrenderActions
{
    class DropWeapon : BaseScript
    {
        public DropWeapon()
        {
            EventHandlers["PSA:DRW"] += new Action<dynamic>(setWeaponDrop);
        }

        private async void setWeaponDrop(dynamic c)
        {
            if (Game.PlayerPed.Exists())
            {
                API.SetPedDropsInventoryWeapon(Game.PlayerPed.Handle, (uint)API.GetSelectedPedWeapon(Game.PlayerPed.Handle), 1, 1, 1, -1);
               Game.PlayerPed.Weapons.Give(WeaponHash.Unarmed, -1, true, true);
            }
        }
    }
}
