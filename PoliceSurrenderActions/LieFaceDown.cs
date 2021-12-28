using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceSurrenderActions
{
    class LieFaceDown : BaseScript
    {
        public static bool FaceDown = false;
        public LieFaceDown() 
        {
            EventHandlers["PSA:FD"] += new Action<dynamic>(ToggleFacedown);
            Main();
        }

        private async void ToggleFacedown(dynamic c)
        {
            if (Game.PlayerPed.Exists())
            {

                if (FaceDown || Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, "missheist_jewel", "2b_guard_onfloor_loop", 3))
                {
                    ClearFacedown();
                }
                else
                {
                    Kneel.ClearKneel();
                    Drag.stopDrag();
                    Game.PlayerPed.Task.PlayAnimation("missheist_jewel", "2b_guard_onfloor_loop", 3, -1, AnimationFlags.Loop | AnimationFlags.AllowRotation);
                    FaceDown = true;
                    Handcuff.cuffed = false;
                    Handsup.HandsupOn = false;
                    
                }
            }
        }

        public static async void ClearFacedown()
        {
            Game.PlayerPed.Task.ClearAnimation("missheist_jewel", "2b_guard_onfloor_loop");
            FaceDown = false;
            reset = true;
        }

        static bool reset = false;
        private async void Main()
        {
            while (true)
            {
                if (FaceDown)
                {
                    bool forceanim = false;
                    while (Function.Call<bool>(Hash.IS_PED_RAGDOLL, Game.PlayerPed))
                    {
                        forceanim = true;
                        await Delay(1000);
                    }

                    if (forceanim || !Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, "missheist_jewel", "2b_guard_onfloor_loop", 3))
                    {
                        Game.PlayerPed.Task.PlayAnimation("missheist_jewel", "2b_guard_onfloor_loop", 3, -1, AnimationFlags.Loop | AnimationFlags.AllowRotation);
                    }
                    await Delay(0);
                }
                else if (!reset && Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, "missheist_jewel", "2b_guard_onfloor_loop", 3))
                {
                    FaceDown = true;
                    await Delay(5);
                } else
                {
                    reset = false;
                    await Delay(5);
                }
            }
        }
    }
}
