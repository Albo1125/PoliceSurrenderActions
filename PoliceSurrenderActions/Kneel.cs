using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceSurrenderActions
{
    class Kneel : BaseScript
    {
        public static bool Kneeling = false;
        public const string KneelDictionary = "random@arrests"; //"random@arrests", "kneeling_arrest_idle"
        public const string KneelAnim = "kneeling_arrest_idle";
        public Kneel()
        {
            EventHandlers["PSA:TKN"] += new Action<dynamic>(ToggleKneeling);
            Main();
        }

        private async void ToggleKneeling(dynamic c)
        {
            if (Game.PlayerPed.Exists())
            {

                if (Kneeling || Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, KneelDictionary, KneelAnim, 3))
                {
                    ClearKneel();
                }
                else
                {
                    LieFaceDown.ClearFacedown();
                    Drag.stopDrag();
                    Game.PlayerPed.Task.PlayAnimation(KneelDictionary, KneelAnim, 3, -1, (AnimationFlags)1);
                    Kneeling = true;
                }
            }
        }

        public static async void ClearKneel()
        {
            Game.PlayerPed.Task.ClearAnimation(KneelDictionary, KneelAnim);
            Kneeling = false;
        }

        private async void Main()
        {
            while (true)
            {
                if (Kneeling)
                {
                    

                    while (Function.Call<bool>(Hash.IS_PED_RAGDOLL, Game.PlayerPed))
                    {
                        await Delay(10);
                    }

                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, Game.PlayerPed, KneelDictionary, KneelAnim, 3))
                    {
                        Game.PlayerPed.Task.PlayAnimation(KneelDictionary, KneelAnim, 3, -1, (AnimationFlags)1);
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
