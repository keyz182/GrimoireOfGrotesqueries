using RimWorld;
using Verse;

namespace GrimoireOfGrotesqueries;

[DefOf]
public static class GrimoireOfGrotesqueriesDefOf
{
    // Remember to annotate any Defs that require a DLC as needed e.g.
    // [MayRequireBiotech]
    // public static GeneDef YourPrefix_YourGeneDefName;

    static GrimoireOfGrotesqueriesDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(GrimoireOfGrotesqueriesDefOf));
}
