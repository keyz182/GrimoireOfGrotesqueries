﻿using UnityEngine;
using Verse;

namespace GrimoireOfGrotesqueries;

public class Settings : ModSettings
{
    //Use Mod.settings.setting to refer to this setting.
    public bool setting = true;

    public void DoWindowContents(Rect wrect)
    {
        var options = new Listing_Standard();
        options.Begin(wrect);

        options.CheckboxLabeled("Sir Hinus' Grimoire of Grotesqueries_Settings_SettingName".Translate(), ref setting);
        options.Gap();

        options.End();
    }

    public override void ExposeData()
    {
        Scribe_Values.Look(ref setting, "setting", true);
    }
}
