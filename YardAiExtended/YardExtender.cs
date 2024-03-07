using Game.Messages;
using Game.State;
using HarmonyLib;
using Model;
using Model.AI;
using System;
using System.Collections.Generic;
using UI.Builder;
using UI.CarInspector;
using UI.Common;
using UnityEngine;


namespace YardAiExtended
{
    [HarmonyPatch]
    public class AIPanelExtender

    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CarInspector), "PopulateAIPanel")]
        static bool PopulateAIPanelPatch(UIPanelBuilder builder, Car ____car, Window ____window)
        {
            builder.FieldLabelWidth = 100f;
            builder.Spacing = 8f;
            AutoEngineerPersistence persistence = new AutoEngineerPersistence(____car.KeyValueObject);
            AutoEngineerMode aimode = Mode();
            int maxSpeed = YardAiExtended.Settings.DefaultYardSpeed;
            int defaultYardSpeed = YardAiExtended.Settings.DefaultYardSpeed;
          
            builder.AddObserver(persistence.ObserveOrders(delegate
            {
                if (aimode != Mode()) {
                    builder.Rebuild();
                }
            }, false));

            builder.AddField("Mode", builder.ButtonStrip(delegate (UIPanelBuilder builder)
            {
                string text = "Manual";
                bool flag = aimode == AutoEngineerMode.Off;
                Action action;
                Action something = null;
                if ((action = something) == null)
                {
                    action = (something = delegate
                    {
                        SetOrdersValue(AutoEngineerMode.Off, null, null, null);
                    });
                }
                builder.AddButtonSelectable(text, flag, action);
                string text2 = "Road";
                bool flag2 = aimode == AutoEngineerMode.Road;
                Action action2;
                Action something2 = null;
                if ((action2 = something2) == null)
                {
                    action2 = (something2 = delegate
                    {
                        SetOrdersValue(AutoEngineerMode.Road, null, null, null);
                    });
                }
                builder.AddButtonSelectable(text2, flag2, action2);
                string text3 = "Yard";
                bool flag3 = aimode == AutoEngineerMode.Yard;
                Action action3;
                Action something3 = null;
                if ((action3 = something3) == null)
                {
                    action3 = (something3 = delegate
                    {
                        SetOrdersValue(AutoEngineerMode.Yard, null, null, null);
                    });
                }
                builder.AddButtonSelectable(text3, flag3, action3);

            }));

            if (!persistence.Orders.Enabled)
            {
                builder.AddExpandingVerticalSpacer();
                return false;
            }

            builder.AddField("Direction", builder.ButtonStrip(delegate (UIPanelBuilder builder)
            {
                builder.AddObserver(persistence.ObserveOrders(delegate
                {
                    builder.Rebuild();
                }, false));
                string text4 = "Reverse";
                bool flag4 = !persistence.Orders.Forward;
                Action action4;
                Action something4 = null;
                if ((action4 = something4) == null)
                {
                    action4 = (something4 = delegate
                    {
                        bool flag5 = false;
                        SetOrdersValue(null, flag5, null, null);
                    });
                }
                builder.AddButtonSelectable(text4, flag4, action4);
                string text5 = "Forward";
                bool flag5 = persistence.Orders.Forward;
                Action action5;
                Action something5 = null;
                if ((action5 = something5) == null)
                {
                    action5 = (something5 = delegate
                    {
                        bool flag6 = true;
                        SetOrdersValue(null , flag6, null, null);
                    });
                }
                builder.AddButtonSelectable(text5 , flag5, action5);
            }));

            if (aimode == AutoEngineerMode.Road)
            {
                RectTransform rectTransform = builder.AddSlider(() => persistence.Orders.MaxSpeedMph / 5f, () => persistence.Orders.MaxSpeedMph.ToString(), delegate (float value)
                {
                    int? num2 = (int)(value * 5f);
                    SetOrdersValue(null, null, num2, null);
                }, 0f, maxSpeed / 5f, true, null);
                builder.AddField("Max Speed", rectTransform);
                builder.AddExpandingVerticalSpacer();
                builder.AddObserver(persistence.ObservePassengerModeStatusChanged(delegate
                {
                    builder.Rebuild();
                }));
                string passengerModeStatus = persistence.PassengerModeStatus;
                if (aimode == AutoEngineerMode.Road && !string.IsNullOrEmpty(passengerModeStatus))
                {
                    builder.AddField("Station Stops", passengerModeStatus).Tooltip("AI Passenger Stops", "When stations are checked on passenger cars in the train, the AI engineer will perform stops as those stations are encountered.");
                }
                builder.AddObserver(persistence.ObservePlannerStatusChanged(delegate
                {
                    builder.Rebuild();
                }));
                builder.AddField("Status", persistence.PlannerStatus);

                BuildContextualOrders(builder, persistence);
            }

            if (aimode == AutoEngineerMode.Yard)
            {
                Vector2 winSize = ____window.contentRectTransform.rect.size;
                Vector3 winPos = ____window.contentRectTransform.position;
                if (winSize.y < 323f)
                {
                    Vector2 winSize2 = new Vector2(winSize.x, winSize.y + 60f);
                    ____window.transform.Translate(0, 60f, 0);
                    ____window.SetContentSize(winSize2);
                }
                
                builder.Spacing = 4f;
                RectTransform control2 = builder.ButtonStrip(delegate (UIPanelBuilder builder1)
                {
                    builder1.AddButton("Stop", delegate
                    {
                        float? distance25 = 0f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("½", delegate
                    {
                        float? distance25 = 6.1f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("1", delegate
                    {
                        float? distance25 = 12.2f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("2", delegate
                    {
                        float? distance25 = 24.4f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("3", delegate
                    {
                        float? distance25 = 36.6f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("5", delegate
                    {
                        float? distance25 = 61f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("7", delegate
                    {
                        float? distance25 = 85.4f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("10", delegate
                    {
                        float? distance25 = 122f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                });
                builder.AddField("", control2);
                RectTransform control3 = builder.ButtonStrip(delegate (UIPanelBuilder builder1)
                {
                    builder1.AddButton("15", delegate
                    {
                        float? distance25 = 183f;
                        SetOrdersValue(null, null, null, distance25);
                    });
                    builder1.AddButton("20", delegate
                    {
                        float? distance25 = 244f;
                        SetOrdersValue(null, null, null, distance25);
                    });
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
                });
                builder.AddField("Car lengths", control3);

                RectTransform control5 = builder.ButtonStrip(delegate (UIPanelBuilder builder1)
                {
                    builder1.AddButton("Very long", delegate
                    {
                        SetOrdersValue(null, null, null, 100000f);
                    });
                });
                builder.AddField("", control5);
                builder.Spacing = 8f;

                if (persistence.Orders.MaxSpeedMph == 0)
                {
                    SetOrdersValue (null, null, defaultYardSpeed, null);
                }
                RectTransform control4 = builder.AddSlider(() => persistence.Orders.MaxSpeedMph, () => persistence.Orders.MaxSpeedMph.ToString(), delegate (float value)
                {
                    int? setSpeed = (int)value;
                    SetOrdersValue(null, null, setSpeed, null);
                }, 1f, (float)MaxSpeedMphForMode(aimode), wholeNumbers: true);
                builder.AddField("Yard Speed", control4);

                builder.AddExpandingVerticalSpacer();

                builder.AddObserver(persistence.ObservePassengerModeStatusChanged(delegate
                {
                    builder.Rebuild();
                }));
                string passengerModeStatus = persistence.PassengerModeStatus;
                if (aimode == AutoEngineerMode.Road && !string.IsNullOrEmpty(passengerModeStatus))
                {
                    builder.AddField("Station Stops", passengerModeStatus).Tooltip("AI Passenger Stops", "When stations are checked on passenger cars in the train, the AI engineer will perform stops as those stations are encountered.");
                }
                builder.AddObserver(persistence.ObservePlannerStatusChanged(delegate
                {
                    builder.Rebuild();
                    #if DEBUG
                    Console.Log("ID: " + ____car.Ident.ToString() + " Dist: " + persistence.ManualStopDistance.ToString() + "OS: " + persistence.Orders.MaxSpeedMph.ToString() + "Vel: " + ____car.velocity.ToString());
                    #endif
                }));
                builder.AddField("Status", persistence.PlannerStatus);

                BuildContextualOrders(builder, persistence);
            }

            void BuildContextualOrders(UIPanelBuilder builder, AutoEngineerPersistence persistence)
            {
                builder.AddObserver(persistence.ObserveContextualOrdersChanged(delegate
                {
                    builder.Rebuild();
                }));
                List<ContextualOrder> contextualOrders = persistence.ContextualOrders;
                if (contextualOrders.Count <= 0)
                {
                    return;
                }
                builder.AddField("", builder.ButtonStrip(delegate (UIPanelBuilder builder)
                {
                    using (List<ContextualOrder>.Enumerator enumerator = contextualOrders.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            ContextualOrder co = enumerator.Current;
                            ValueTuple<string, string> valueTuple;
                            switch (co.Order)
                            {
                                case ContextualOrder.OrderValue.PassSignal:
                                    valueTuple = new ValueTuple<string, string>("Pass Signal", "Pass the signal at restricted speed.");
                                    break;
                                case ContextualOrder.OrderValue.PassFlare:
                                    valueTuple = new ValueTuple<string, string>("Pass Fusee", "Pass the fusee.");
                                    break;
                                case ContextualOrder.OrderValue.ResumeSpeed:
                                    valueTuple = new ValueTuple<string, string>("Resume Speed", "Discard speed restriction.");
                                    break;
                                default:
                                    valueTuple = new ValueTuple<string, string>("(Error)", "");
                                    break;
                            }
                            ValueTuple<string, string> valueTuple2 = valueTuple;
                            string item = valueTuple2.Item1;
                            string item2 = valueTuple2.Item2;
                            builder.AddButton(item, delegate
                            {
                                StateManager.ApplyLocal(new AutoEngineerContextualOrder(____car.id, co.Order, co.Context));
                            }).Tooltip(item, item2);
                        }
                    }
                }, 8));
            }

            void SendAutoEngineerCommand(AutoEngineerMode mode, bool forward, int maxSpeedMph, float? distance)
            {
                StateManager.ApplyLocal(new AutoEngineerCommand(____car.id, mode, forward, maxSpeedMph, distance));
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
                return mode switch
                {
                    AutoEngineerMode.Road => 45,
                    AutoEngineerMode.Yard => 25,
                    AutoEngineerMode.Off => 0,
                    _ => throw new ArgumentOutOfRangeException("mode", mode, null),
                };
            }

            void SetOrdersValue(AutoEngineerMode? mode, bool? forward, int? maxSpeedMph, float? distance)
            {
                Orders orders = persistence.Orders;
                if (!orders.Enabled && mode.HasValue && mode.Value != 0 && !maxSpeedMph.HasValue)
                {
                    float num2 = ____car.velocity * 2.23694f;
                    float num3 = Mathf.Abs(num2);
                    maxSpeedMph = ((num2 > 0.1f) ? (Mathf.CeilToInt(num3 / 5f) * 5) : 0);
                    forward = num2 >= -0.1f;
                }
                if (mode == AutoEngineerMode.Yard)
                {
                    if (persistence.Orders.MaxSpeedMph > 0)
                    {
                        maxSpeedMph = persistence.Orders.MaxSpeedMph;
                    }
                    else
                    {
                        maxSpeedMph = defaultYardSpeed;
                    }
                }
                AutoEngineerMode mode3 = mode ?? Mode();
                int maxSpeedMph2 = Mathf.Min(maxSpeedMph ?? orders.MaxSpeedMph, MaxSpeedMphForMode(mode3));
                SendAutoEngineerCommand(mode3, forward ?? orders.Forward, maxSpeedMph2, distance);
            }
            return false;
        }
    }
}