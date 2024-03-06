using HarmonyLib;
using Railloader;
using UI.Builder;
using UnityEngine;

namespace YardAiExtended
{
    internal class Settings
    {
        public int DefaultYardSpeed = 15;
        public Settings()
        {
        }
    }

    public class YardAiExtended : PluginBase, IModTabHandler
    {
        private readonly IModDefinition self;
        private readonly IModdingContext moddingContext;
        internal static Settings Settings;
        public YardAiExtended(IModDefinition self, IModdingContext moddingContext)
        {
            YardAiExtended.Settings = moddingContext.LoadSettingsData<Settings>(self.Id) ?? new Settings();
            this.self = self;
            this.moddingContext = moddingContext;
            new Harmony("wexp.YardAiExtended").PatchAll(GetType().Assembly);
        }

        public void ModTabDidClose()
        {
            this.moddingContext.SaveSettingsData<Settings>(this.self.Id, YardAiExtended.Settings);
        }

        public void ModTabDidOpen(UIPanelBuilder builder)
        {
            builder.AddField("Default Yard mode speed: ", builder.AddSlider(() => YardAiExtended.Settings.DefaultYardSpeed, () => YardAiExtended.Settings.DefaultYardSpeed.ToString(), delegate (float r)
            {
                YardAiExtended.Settings.DefaultYardSpeed = Mathf.CeilToInt(r);
            }, 1f, 25f, true));
        }
    }
}
