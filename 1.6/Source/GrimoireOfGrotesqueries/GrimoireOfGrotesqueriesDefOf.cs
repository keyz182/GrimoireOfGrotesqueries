using RimWorld;
using Verse;

namespace GrimoireOfGrotesqueries;

[DefOf]
public static class GrimoireOfGrotesqueriesDefOf
{
    // Remember to annotate any Defs that require a DLC as needed e.g.
    // [MayRequireBiotech]
    // public static GeneDef YourPrefix_YourGeneDefName;

    public static ThingDef GOG_Stampede_PawnFlyer;
    public static SoundDef GOG_StrangeWalker;
    public static HediffDef GOG_CoveredInSlime;
    public static HediffDef GOG_Blinded;

    static GrimoireOfGrotesqueriesDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(GrimoireOfGrotesqueriesDefOf));
}
