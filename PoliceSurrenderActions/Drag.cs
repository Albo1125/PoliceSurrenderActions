using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceSurrenderActions
{
    class Drag : BaseScript
    {
        static Ped dragger;
        private static Model[] otherSideModels = new Model[] { };

        public Drag()
        {
            EventHandlers["PSA:DRAG"] += new Action<int>(toggleDrag);
            EventHandlers["PSA:STOPDRAG"] += new Action<dynamic>((dynamic c) =>
            {
                stopDrag();
            });
        }

        public static void stopDrag()
        {
            dragger = null;
            if (Game.Player.Character.IsAttached())
            {
                Game.Player.Character.Detach();
            }
        }

        private async void toggleDrag(int draggerId)
        {
            if (dragger != null && dragger.Exists())
            {
                stopDrag();
            }
            else
            {
                stopDrag();
                Ped tempdragger = Players[draggerId].Character;
                if (Game.PlayerPed.Exists() && tempdragger.Exists() && Game.PlayerPed != tempdragger && !tempdragger.IsAttached())
                {
                    if (Vector3.Distance(Game.PlayerPed.Position, tempdragger.Position) < 5)
                    {
                        dragger = tempdragger;
                        MainDrag();
                    }
                    else
                    {
                        TriggerServerEvent("PSA:TooFarAway", draggerId);
                    }
                }
            }
        }

        private async void MainDrag()
        {
            while (dragger != null && dragger.Exists() && Game.Player.Character.IsAlive && dragger.IsAlive)
            {
                if (!dragger.IsAttachedTo(LocalPlayer.Character))
                {
                    Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, LocalPlayer.Character, dragger, 11816, 0.54, 0.54, 0.0, 0.0, 0.0, 0.0, false, false, false, false, 2, true);
                }
                if ((dragger.CurrentVehicle != null && dragger.CurrentVehicle.Exists()) || (dragger.VehicleTryingToEnter != null && dragger.VehicleTryingToEnter.Exists()))
                {
                    placeInVehicle();

                }
                await Delay(10);
            }
            stopDrag();            
        }

        private async void placeInVehicle()
        {
            Vehicle veh = getVehicle();
            if (veh != null && veh.Exists())
            {
                if (otherSideModels.Contains(veh.Model))
                {
                    if (veh.IsSeatFree(VehicleSeat.LeftRear))
                    {
                        stopDrag();
                        //LocalPlayer.Character.Task.EnterVehicle(veh, VehicleSeat.LeftRear, 4000);
                        LocalPlayer.Character.SetIntoVehicle(veh, VehicleSeat.LeftRear);
                        
                    }
                    else if (veh.IsSeatFree(VehicleSeat.RightRear))
                    {
                        stopDrag();
                        LocalPlayer.Character.SetIntoVehicle(veh, VehicleSeat.RightRear);

                    }
                }
                else
                {
                    if (veh.IsSeatFree(VehicleSeat.RightRear))
                    {
                        stopDrag();
                        //LocalPlayer.Character.Task.EnterVehicle(veh, VehicleSeat.RightRear, 4000);
                        LocalPlayer.Character.SetIntoVehicle(veh, VehicleSeat.RightRear);

                    }
                    else if (veh.IsSeatFree(VehicleSeat.LeftRear))
                    {
                        stopDrag();
                        //LocalPlayer.Character.Task.EnterVehicle(veh, VehicleSeat.LeftRear, 4000);
                        LocalPlayer.Character.SetIntoVehicle(veh, VehicleSeat.LeftRear);
                    }
                }
            }
        }

        private static Vehicle getVehicle()
        {
            if (dragger.CurrentVehicle != null && dragger.CurrentVehicle.Exists())
            {
                return dragger.CurrentVehicle;
            }
            if (dragger.VehicleTryingToEnter != null && dragger.VehicleTryingToEnter.Exists())
            {
                return dragger.VehicleTryingToEnter;
            }
            return null;
        }
    }
}
