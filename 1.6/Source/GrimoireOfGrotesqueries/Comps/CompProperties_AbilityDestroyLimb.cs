using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GrimoireOfGrotesqueries.Comps;

public class CompProperties_AbilityDestroyLimb: CompProperties_AbilityEffect
{
    public List<BodyPartGroupDef> TargetGroups;
    public CompProperties_AbilityDestroyLimb()
    {
        compClass = typeof(CompAbilityEffect_DestroyLimb);
    }
}
