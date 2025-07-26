using GrimoireOfGrotesqueries.Rendering;
using HarmonyLib;
using Verse;

namespace GrimoireOfGrotesqueries.Patches;

[HarmonyPatch(typeof(PawnDrawParms))]
public static class PawnDrawParms_Patch
{
    [HarmonyPatch(nameof(PawnDrawParms.ShouldRecache))]
    [HarmonyPostfix]
    public static void ShouldRecache_Patch(PawnDrawParms __instance, ref bool __result)
    {
        if(__result) return;

        PawnSlimeDrawer drawer = PawnRenderNodeWorker_OverlaySlime.DrawerForPawn(__instance.pawn);
        __result = drawer.needRecache && __result;
        drawer.needRecache = false;
    }
}
