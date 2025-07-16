using Verse;

namespace GrimoireOfGrotesqueries;

public class CompProperties_StrangeWalker: CompProperties
{
    public int ticksToRecalculate = 60;
    public int tentacleCount = 7;
    public float maxLength = 4f;
    public float searchRadius = 6f;

    public CompProperties_StrangeWalker()
    {
        compClass = typeof(CompStrangeWalker);
    }

}
