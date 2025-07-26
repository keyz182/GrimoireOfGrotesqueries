using RimWorld;
using Verse;
using Verse.AI;

namespace GrimoireOfGrotesqueries.Jobs;

public class JobGiver_AIFightDowned: JobGiver_AIAbilityFight
{
    protected override Thing FindAttackTarget(Pawn pawn)
    {
        TargetScanFlags flags = TargetScanFlags.NeedLOSToPawns | TargetScanFlags.NeedReachableIfCantHitFromMyPos | TargetScanFlags.NeedThreat | TargetScanFlags.NeedAutoTargetable;

        return (Thing) AttackTargetFinder.BestAttackTarget(pawn, flags,  x => ExtraTargetValidator(pawn, x), maxDist: targetAcquireRadius, locus: GetFlagPosition(pawn), maxTravelRadiusFromLocus: GetFlagRadius(pawn), onlyRanged: OnlyUseRangedSearch);
    }
}
