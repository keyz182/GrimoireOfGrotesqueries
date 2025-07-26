using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace GrimoireOfGrotesqueries.Comps;

public class CompAbilityEffect_DestroyLimb: CompAbilityEffect
{
    public CompProperties_AbilityDestroyLimb Props => props as CompProperties_AbilityDestroyLimb;

    public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
    {
        Pawn pawn = target.Pawn;

        if(pawn is null) return;

        if(Props.TargetGroups.NullOrEmpty()) return;
        List<BodyPartRecord> allParts = pawn.RaceProps.body.AllParts;

        BodyPartRecord targetPart = Props.TargetGroups.SelectMany(g => allParts.Where(p => p.IsInGroup(g))).Distinct().Where(part=>pawn.health.hediffSet.PartIsMissing(part)).RandomElementWithFallback();

        if(targetPart == null) return;

        Hediff_MissingPart hediff = HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, pawn, targetPart) as Hediff_MissingPart;

        pawn.health.AddHediff(hediff, targetPart);

    }
}
