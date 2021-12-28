using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceSurrenderActions
{
    public class Handcuff : BaseScript
    {
        public static bool cuffed = false;
        private static bool frontCuffs = false;
        private static int[] disableControlIDs = new int[] { 24, 257, 263, 264, 142, 141, 140, 58 };
        public const string frontCuffDict = "anim@move_m@prisoner_cuffed";
        public const string frontCuffAnim = "idle";

        public const string backCuffDict = "mp_arresting";
        public const string backCuffAnim = "idle";

        public Handcuff()
        {
            EventHandlers["PSA:TCfs"] += new Action<int, bool>(ToggleCuffs);
            Main();
        }

        private async void ToggleCuffs(int cufferID, bool front)
        {
            Ped cuffer = Players[cufferID].Character;
            if (Game.PlayerPed.Exists() && cuffer.Exists())
            {
                if (Vector3.Distance(Game.PlayerPed.Position, cuffer.Position) < 10)
                {
                    if (cuffed || Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, backCuffDict, backCuffAnim, 3) || Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, frontCuffDict, frontCuffAnim, 3))
                    {
                        Game.PlayerPed.Task.ClearSecondary();
                        cuffed = false;
                        foreach (int i in disableControlIDs)
                        {
                            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, i, false);
                        }
                    }
                    else
                    {
                        LieFaceDown.ClearFacedown();
                        Handsup.HandsupOn = false;
                        if (front)
                        {
                            Game.PlayerPed.Task.PlayAnimation(frontCuffDict, frontCuffAnim, 8, -1, (AnimationFlags)49);
                        }
                        else
                        {
                            Game.PlayerPed.Task.PlayAnimation(backCuffDict, backCuffAnim, 8, -1, (AnimationFlags)49);
                        }
                        frontCuffs = front;
                        cuffed = true;                                               
                    }
                }
                else
                {
                    TriggerServerEvent("PSA:TooFarAway", cufferID);
                }
            }
        }

        private async void Main()
        {
            while (true)
            {
                if (cuffed)
                {                   
                    while (Function.Call<bool>(Hash.IS_PED_RAGDOLL, Game.PlayerPed))
                    {
                        await Delay(10);
                    }
                    
                    if (!frontCuffs && !Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, backCuffDict, backCuffAnim, 3))
                    {
                        Game.PlayerPed.Task.PlayAnimation(backCuffDict, backCuffAnim, 8, -1, (AnimationFlags)49);
                    }
                    else if (frontCuffs && !Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, frontCuffDict, frontCuffAnim, 3))
                    {
                        Game.PlayerPed.Task.PlayAnimation(frontCuffDict, frontCuffAnim, 8, -1, (AnimationFlags)49);
                    }
                    foreach (int i in disableControlIDs)
                    {
                        Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, i, true);
                    }

                    if (Game.PlayerPed.Weapons.Current.Hash != WeaponHash.Unarmed)
                    {
                        Game.PlayerPed.Weapons.Give(WeaponHash.Unarmed, -1, true, true);
                    }
                    await Delay(0);

                }
                else
                {
                    await Delay(10);
                }
            }
        }
    }
}
