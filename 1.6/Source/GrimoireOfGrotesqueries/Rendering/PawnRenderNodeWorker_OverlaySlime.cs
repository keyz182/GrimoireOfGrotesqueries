using System.Collections.Concurrent;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GrimoireOfGrotesqueries.Rendering;

public class PawnRenderNodeWorker_OverlaySlime : PawnRenderNodeWorker_Overlay
{
    public static ConcurrentDictionary<Pawn, PawnSlimeDrawer> RendererCache = new();

    public static PawnSlimeDrawer DrawerForPawn(Pawn pawn)
    {
        if (!RendererCache.ContainsKey(pawn)) RendererCache[pawn] = new PawnSlimeDrawer(pawn);
        return RendererCache[pawn];
    }

    protected override PawnOverlayDrawer OverlayDrawer(Pawn pawn)
    {
        return DrawerForPawn(pawn);
    }

    public override bool ShouldListOnGraph(PawnRenderNode node, PawnDrawParms parms)
    {
        return DrawerForPawn(parms.pawn).CoveredInSlime;
    }

    public override bool CanDrawNow(PawnRenderNode node, PawnDrawParms parms)
    {
        bool shouldDraw = DrawerForPawn(parms.pawn).CoveredInSlime;
        return shouldDraw && base.CanDrawNow(node, parms) && parms.rotDrawMode == RotDrawMode.Fresh;
    }
}
