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

    static GrimoireOfGrotesqueriesDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(GrimoireOfGrotesqueriesDefOf));
}
