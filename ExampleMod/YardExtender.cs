using Game.Messages;
using Game.State;
using HarmonyLib;
using Model;
using Model.AI;
using System;
using System.Reflection;
using UI.Builder;
using UI.CarInspector;
using UnityEngine;


namespace YardAiExtended
{
    [HarmonyPatch(typeof(CarInspector), "PopulateAIPanel")]
    public class AIPanelExtender
    {
        static void Postfix(UIPanelBuilder builder, CarInspector __instance)
        {
            var carField = typeof(CarInspector).GetField("_car", BindingFlags.NonPublic | BindingFlags.Instance);
            var car = carField.GetValue(__instance) as Car;

            builder.FieldLabelWidth = 100f;
            builder.Spacing = 8f;
            AutoEngineerPersistence persistence = new AutoEngineerPersistence(car.KeyValueObject);
            var aimode = Mode();


            int num = MaxSpeedMphForMode(aimode);
            if (aimode == AutoEngineerMode.Yard)
            {
                RectTransform control2 = builder.ButtonStrip(delegate (UIPanelBuilder builder1)
                {
                    builder1.AddButton("30", delegate
                    {
                        float? distance25 = 366f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("40", delegate
                    {
                        float? distance25 = 488f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("50", delegate
                    {
                        float? distance25 = 610f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("70", delegate
                    {
                        float? distance25 = 854f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("100", delegate
                    {
                        float? distance25 = 1220f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("200", delegate
                    {
                        float? distance25 = 2440f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                });

                builder.AddField("YAE", control2);


                /*     RectTransform control3 = builder.AddSlider(() => persistence.Orders.MaxSpeedMph, delegate
                     {
                         int maxSpeedMph25 = persistence.Orders.MaxSpeedMph;
                         return maxSpeedMph25.ToString(); 
                     }, delegate (float value)
                     {
                         int? maxSpeedMph25 = (int)(value);
                         SetOrdersValue(null, null, maxSpeedMph25, null);
                     }, 0f, num, wholeNumbers: true);
                     builder.AddField("Max Yard Speed", control3);
                 */

            }


            void SendAutoEngineerCommand(AutoEngineerMode mode, bool forward, int maxSpeedMph, float? distance)
            {
                StateManager.ApplyLocal(new AutoEngineerCommand(car.id, mode, forward, maxSpeedMph, distance));
            }

            AutoEngineerMode Mode()
            {
                Orders orders2 = persistence.Orders;
                if (!orders2.Enabled)
                {
                    return AutoEngineerMode.Off;
                }
                if (!orders2.Yard)
                {
                    return AutoEngineerMode.Road;
                }
                return AutoEngineerMode.Yard;
            }

            int MaxSpeedMphForMode(AutoEngineerMode mode)
            {
                switch (mode)
                {
                    case AutoEngineerMode.Road:
                        return 45;
                    case AutoEngineerMode.Yard:
                        return 25;
                    case AutoEngineerMode.Off:
                        return 0;
                    default:
                        throw new ArgumentOutOfRangeException("mode", mode, null);
                }
            }


            void SetOrdersValue(AutoEngineerMode? mode, bool? forward, int? maxSpeedMph, float? distance)
            {
                Orders orders = persistence.Orders;
                if (!orders.Enabled && mode.HasValue && mode.Value != 0 && !maxSpeedMph.HasValue)
                {
                    float num2 = car.velocity * 2.23694f;
                    float num3 = Mathf.Abs(num2);
                    maxSpeedMph = ((num2 > 0.1f) ? (Mathf.CeilToInt(num3 / 5f) * 5) : 0);
                    forward = num2 >= -0.1f;
                }
                if (mode == AutoEngineerMode.Yard)
                {
                    maxSpeedMph = MaxSpeedMphForMode(AutoEngineerMode.Yard);
                }
                AutoEngineerMode mode3 = mode ?? Mode();
                int maxSpeedMph2 = Mathf.Min(maxSpeedMph ?? orders.MaxSpeedMph, MaxSpeedMphForMode(mode3));
                SendAutoEngineerCommand(mode3, forward ?? orders.Forward, maxSpeedMph2, distance);
            }
        }

    }
}


