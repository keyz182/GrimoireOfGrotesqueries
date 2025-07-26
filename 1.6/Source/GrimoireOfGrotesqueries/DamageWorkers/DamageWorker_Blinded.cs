using Verse;

namespace GrimoireOfGrotesqueries.DamageWorkers;

public class DamageWorker_Blinded: DamageWorker
{
    public override DamageResult Apply(DamageInfo dinfo, Thing victim)
    {
        DamageResult damageResult = base.Apply(dinfo, victim);
        if(victim is Pawn pawn) pawn.health?.AddHediff(GrimoireOfGrotesqueriesDefOf.GOG_Blinded);
        return damageResult;
    }
}
