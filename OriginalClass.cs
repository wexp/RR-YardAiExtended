private void PopulateAIPanel(UIPanelBuilder builder)
{
    CarInspector.<> c__DisplayClass50_0 internal1 = new CarInspector.<> c__DisplayClass50_0();
    internal1.<> 4__this = this;
    internal1.builder = builder;
    internal1.builder.FieldLabelWidth = new float?((float)100);
    internal1.builder.Spacing = 8f;
    internal1.persistence = new AutoEngineerPersistence(this._car.KeyValueObject);
    internal1.mode = internal1.Mode() | 1();
    internal1.builder.AddObserver(internal1.persistence.ObserveOrders(delegate (Orders _)
    {
    if (base.Mode() | 1() != internal1.mode)

        {
        internal1.builder.Rebuild();
    }
}, false))
CS$<>8__locals1.builder.AddField("Mode", CS$<>8__locals1.builder.ButtonStrip(delegate (UIPanelBuilder builder)
{
    string text = "Manual";
    bool flag = internal1.mode == AutoEngineerMode.Off;
    Action action;
    if ((action = something) == null)
		{
        action = (something = delegate
        {
            base.< PopulateAIPanel > g__SetOrdersValue | 3(new AutoEngineerMode?(AutoEngineerMode.Off), null, null, null);
        });
    }
    builder.AddButtonSelectable(text, flag, action);
    string text2 = "Road";
    bool flag2 = internal1.mode == AutoEngineerMode.Road;
    Action action2;
    if ((action2 = internal1.<> 9__10) == null)
		{
        action2 = (internal1.<> 9__10 = delegate
        {
            base.< PopulateAIPanel > g__SetOrdersValue | 3(new AutoEngineerMode?(AutoEngineerMode.Road), null, null, null);
        });
    }
    builder.AddButtonSelectable(text2, flag2, action2);
    string text3 = "Yard";
    bool flag3 = internal1.mode == AutoEngineerMode.Yard;
    Action action3;
    if ((action3 = internal1.<> 9__11) == null)
		{
        action3 = (internal1.<> 9__11 = delegate
        {
            base.< PopulateAIPanel > g__SetOrdersValue | 3(new AutoEngineerMode?(AutoEngineerMode.Yard), null, null, null);
        });
    }
    builder.AddButtonSelectable(text3, flag3, action3);
}, 8));
if (!internal1.persistence.Orders.Enabled)
	{
    internal1.builder.AddExpandingVerticalSpacer();
    return;
}
CS$<>8__locals1.builder.AddField("Direction", CS$<>8__locals1.builder.ButtonStrip(delegate (UIPanelBuilder builder)
{
    CarInspector.<> c__DisplayClass50_1 CS$<> 8__locals2 = new CarInspector.<> c__DisplayClass50_1();
    CS$<> 8__locals2.builder = builder;
    CS$<> 8__locals2.builder.AddObserver(internal1.persistence.ObserveOrders(delegate (Orders _)
    {
        CS$<> 8__locals2.builder.Rebuild();
    }, false));
    CarInspector.<> c__DisplayClass50_1 CS$<> 8__locals3 = CS$<> 8__locals2;
    string text4 = "Reverse";
    bool flag4 = !internal1.persistence.Orders.Forward;
    Action action4;
    if ((action4 = internal1.<> 9__13) == null)
		{
        action4 = (internal1.<> 9__13 = delegate
        {
            bool? flag5 = new bool?(false);
            base.< PopulateAIPanel > g__SetOrdersValue | 3(null, flag5, null, null);
        });
    }
    CS$<> 8__locals3.builder.AddButtonSelectable(text4, flag4, action4);
    CarInspector.<> c__DisplayClass50_1 CS$<> 8__locals4 = CS$<> 8__locals2;
    string text5 = "Forward";
    bool forward = internal1.persistence.Orders.Forward;
    Action action5;
    if ((action5 = internal1.<> 9__14) == null)
		{
        action5 = (internal1.<> 9__14 = delegate
        {
            bool? flag6 = new bool?(true);
            base.< PopulateAIPanel > g__SetOrdersValue | 3(null, flag6, null, null);
        });
    }
    CS$<> 8__locals4.builder.AddButtonSelectable(text5, forward, action5);
}, 8));
if (internal1.mode == AutoEngineerMode.Road)
	{
    int num = CarInspector.< PopulateAIPanel > g__MaxSpeedMphForMode | 50_0(internal1.mode);
    RectTransform rectTransform = internal1.builder.AddSlider(() => (float)(internal1.persistence.Orders.MaxSpeedMph / 5), () => internal1.persistence.Orders.MaxSpeedMph.ToString(), delegate (float value)
    {
        int? num2 = new int?((int)(value * 5f));
        base.< PopulateAIPanel > g__SetOrdersValue | 3(null, null, num2, null);
    }, 0f, (float)(num / 5), true, null);
    internal1.builder.AddField("Max Speed", rectTransform);
}
if (internal1.mode == AutoEngineerMode.Yard)
	{
    RectTransform rectTransform2 = internal1.builder.ButtonStrip(delegate (UIPanelBuilder builder)
    {
    string text6 = "Stop";
    Action action6;
    if ((action6 = internal1.<> 9__19) == null)
			{
        action6 = (internal1.<> 9__19 = delegate
        {
            float? num3 = new float?(0f);
            base.< PopulateAIPanel > g__SetOrdersValue | 3(null, null, null, num3);
        });
    }
    builder.AddButton(text6, action6);
    string text7 = "½";
    Action action7;
    if ((action7 = internal1.<> 9__20) == null)
			{
        action7 = (internal1.<> 9__20 = delegate
        {
            float? num4 = new float?(6.1f);
            base.< PopulateAIPanel > g__SetOrdersValue | 3(null, null, null, num4);
        });
    }
    builder.AddButton(text7, action7);
    string text8 = "1";
    Action action8;
    if ((action8 = internal1.<> 9__21) == null)
			{
        action8 = (internal1.<> 9__21 = delegate
        {
            float? num5 = new float?(12.2f);
            base.< PopulateAIPanel > g__SetOrdersValue | 3(null, null, null, num5);
        });
    }
    builder.AddButton(text8, action8);
    string text9 = "2";
    Action action9;
    if ((action9 = internal1.<> 9__22) == null)
			{
        action9 = (internal1.<> 9__22 = delegate
        {
            float? num6 = new float?(24.4f);
            base.< PopulateAIPanel > g__SetOrdersValue | 3(null, null, null, num6);
        });
    }
    builder.AddButton(text9, action9);
    string text10 = "5";
    Action action10;
    if ((action10 = internal1.<> 9__23) == null)
			{
        action10 = (internal1.<> 9__23 = delegate
        {
            float? num7 = new float?(61f);
            base.< PopulateAIPanel > g__SetOrdersValue | 3(null, null, null, num7);
        });
    }
    builder.AddButton(text10, action10);
    string text11 = "10";
    Action action11;
    if ((action11 = internal1.<> 9__24) == null)
			{
        action11 = (internal1.<> 9__24 = delegate
        {
            float? num8 = new float?(122f);
            base.< PopulateAIPanel > g__SetOrdersValue | 3(null, null, null, num8);
        });
    }
    builder.AddButton(text11, action11);
    string text12 = "20";
    Action action12;
    if ((action12 = internal1.<> 9__25) == null)
			{
        action12 = (internal1.<> 9__25 = delegate
        {
            float? num9 = new float?(244f);
            base.< PopulateAIPanel > g__SetOrdersValue | 3(null, null, null, num9);
        });
    }
    builder.AddButton(text12, action12);
}, 4);
CS$<>8__locals1.builder.AddField("Car Lengths", rectTransform2);
	}
	CS$<>8__locals1.builder.AddExpandingVerticalSpacer();
CS$<>8__locals1.builder.AddObserver(CS$<>8__locals1.persistence.ObservePassengerModeStatusChanged(delegate
{
    internal1.builder.Rebuild();
}));
string passengerModeStatus = CS$<>8__locals1.persistence.PassengerModeStatus;
if (internal1.mode == AutoEngineerMode.Road && !string.IsNullOrEmpty(passengerModeStatus))
	{
    internal1.builder.AddField("Station Stops", passengerModeStatus).Tooltip("AI Passenger Stops", "When stations are checked on passenger cars in the train, the AI engineer will perform stops as those stations are encountered.");
}
CS$<>8__locals1.builder.AddObserver(CS$<>8__locals1.persistence.ObservePlannerStatusChanged(delegate
{
    internal1.builder.Rebuild();
}));
CS$<>8__locals1.builder.AddField("Status", CS$<>8__locals1.persistence.PlannerStatus);
this.BuildContextualOrders(internal1.builder, internal1.persistence);
}