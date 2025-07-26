using GrimoireOfGrotesqueries.Rendering;
using Verse;

namespace GrimoireOfGrotesqueries.Hediffs;

public class Hediff_CoveredInSlime : HediffWithComps
{
    public override void PostAdd(DamageInfo? dinfo)
    {
        base.PostAdd(dinfo);
        PawnRenderNodeWorker_OverlaySlime.DrawerForPawn(pawn).CoveredInSlime = true;
    }

    public override void PostRemoved()
    {
        base.PostRemoved();
        PawnRenderNodeWorker_OverlaySlime.DrawerForPawn(pawn).CoveredInSlime =  pawn.health.hediffSet.HasHediff(GrimoireOfGrotesqueriesDefOf.GOG_CoveredInSlime);
    }

    public override void ExposeData()
    {
        base.ExposeData();
        if (Scribe.mode != LoadSaveMode.PostLoadInit)
            return;
        PawnRenderNodeWorker_OverlaySlime.DrawerForPawn(pawn).CoveredInSlime = true;
    }
}
