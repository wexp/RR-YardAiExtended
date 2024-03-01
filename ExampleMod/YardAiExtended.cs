using HarmonyLib;
using Railloader;

namespace YardAiExtended
{
    public class YardAiExtended : PluginBase
    {
        public YardAiExtended()
        {
            new Harmony("wexp.YardAiExtended").PatchAll(GetType().Assembly);
        }
    }
}
