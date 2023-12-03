using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace OnlyGoldBars
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class OnlyGoldBars : BaseUnityPlugin
    {
        public static bool loaded;
        private const string modGUID = "Alphonso.OnlyGoldBars";
        private const string modName = "OnlyGoldBars";
        private const string modVersion = "1.0.1";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static OnlyGoldBars Instance;
        public static ManualLogSource mls;
        private void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource("OnlyGoldBars");
            // Plugin startup logic
            mls.LogInfo("Loaded OnlyGoldBars and applying patches.");
            harmony.PatchAll(typeof(OnlyGoldBars));
            mls = Logger;
        }

        [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.LoadNewLevel))]
        [HarmonyPrefix]
        static bool Only_Gold_Bars(ref SelectableLevel newLevel)
        {

            foreach (var item in newLevel.spawnableScrap)
            {
                item.rarity = 0;
                if (item.spawnableItem.itemName == "Gold bar")
                {
                    item.rarity = 999;
                }
            }
            return true;
        }

    }
}
