using Verse;
using UnityEngine;
using HarmonyLib;

namespace GrimoireOfGrotesqueries;

public class GrimoireOfGrotesqueriesMod : Mod
{
    public static Settings settings;

    public GrimoireOfGrotesqueriesMod(ModContentPack content) : base(content)
    {
        Log.Message("Hello world from Sir Hinus' Grimoire of Grotesqueries");

        // initialize settings
        settings = GetSettings<Settings>();
#if DEBUG
        Harmony.DEBUG = true;
#endif
        Harmony harmony = new Harmony("keyz182.rimworld.GrimoireOfGrotesqueries.main");
        harmony.PatchAll();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        settings.DoWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "Sir Hinus' Grimoire of Grotesqueries_SettingsCategory".Translate();
    }
}
