using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceSurrenderActions
{
    class Handsup : BaseScript
    {
        public static bool HandsupOn = false;
        public Handsup()
        {
            EventHandlers["PSA:THU"] += new Action<dynamic>(ToggleHandsup);
            Main();
        }

        private async void ToggleHandsup(dynamic c)
        {
            if (Game.PlayerPed.Exists())
            {

                if (HandsupOn || Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, "random@getawaydriver", "random@getawaydriver", 3))
                {
                    Game.PlayerPed.Task.ClearSecondary();
                    HandsupOn = false;
                }
                else
                {
                    LieFaceDown.ClearFacedown();                   
                    Handcuff.cuffed = false;
                    Game.PlayerPed.Task.PlayAnimation("random@getawaydriver", "idle_2_hands_up", 8, -1, (AnimationFlags)50);
                    HandsupOn = true;                                     
                }
            }
        }

        private async void Main()
        {
            while (true)
            {
                if (HandsupOn)
                {
                    while (Function.Call<bool>(Hash.IS_PED_RAGDOLL, Game.PlayerPed))
                    {
                        await Delay(10);
                    }

                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, "random@getawaydriver", "idle_2_hands_up", 3))
                    {
                        Game.PlayerPed.Task.PlayAnimation("random@getawaydriver", "idle_2_hands_up", 8, -1, (AnimationFlags)50);
                    }
                    await Delay(0);
                }
                else
                {
                    await Delay(5);
                }
            }
        }
    }
}
