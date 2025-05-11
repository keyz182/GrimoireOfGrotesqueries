using RimWorld;

namespace GrimoireOfGrotesqueries;

public class Verb_Charge : Verb_Jump
{
    protected override bool TryCastShot()
    {
        return JumpUtility.DoJump(CasterPawn, currentTarget, ReloadableCompSource, verbProps, pawnFlyerOverride: GrimoireOfGrotesqueriesDefOf.GOG_Stampede_PawnFlyer);
    }
}
