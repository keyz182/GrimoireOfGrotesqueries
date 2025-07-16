using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace GrimoireOfGrotesqueries;

[StaticConstructorOnStartup]
public class CompStrangeWalker: ThingComp
{
    public class Tentacle(Pawn parent, float searchRadius, float maxLength)
    {
        public Vector3 target;
        public int lastRecalculatedAt = 0;
        public Pawn parent = parent;
        public float searchRadius = searchRadius;
        public float maxLength = maxLength;
        public IntVec3 nextCell;

        public void Tick()
        {
            if (Find.TickManager.TicksGame - lastRecalculatedAt > 30)
            {
                Recalculate();
            }
        }

        public void Recalculate()
        {
            if (parent.pather.nextCell.IsValid) nextCell = parent.pather.nextCell;

            Vector3 pathingDirection = nextCell.ToVector3() - parent.Position.ToVector3();
            pathingDirection.Normalize();


            if (target != Vector3.zero && pathingDirection == Vector3.zero)
                return;

            if (target != Vector3.zero && pathingDirection != Vector3.zero)
            {
                Vector3 targetDirection =  target - parent.Position.ToVector3();
                float dot = Vector3.Dot(targetDirection, pathingDirection);

                if(dot > 0) return;
            }

            if(target.ToIntVec3().DistanceTo(parent.Position) < maxLength) return;

            pathingDirection *= searchRadius;

            IntVec3 targetCell = IntVec3.Invalid;

            for (var attempt = 0; attempt < 10; attempt++){
                if(GenRadial.RadialCellsAround(parent.Position + pathingDirection.ToIntVec3(), searchRadius, true).Where(c=>c.InBounds(parent.Map)).TryRandomElement(out targetCell)) break;
            }

            if (!targetCell.IsValid)
            {
                ModLog.Warn("Failed to find a valid target cell for strange walker");
                return;
            }

            Vector3 newTarget = targetCell.ToVector3();
            // slightly randomize position
            newTarget.x += (Rand.Value * 2) - 1;
            newTarget.z += (Rand.Value * 2) - 1;

            target =  newTarget;

            GrimoireOfGrotesqueriesDefOf.GOG_StrangeWalker.PlayOneShot(SoundInfo.InMap(new TargetInfo(newTarget.ToIntVec3(), parent.Map)));
        }

        public void Draw()
        {
            GenDraw.DrawLineBetween(parent.DrawPos.Yto0(), target.Yto0(), AltitudeLayer.PawnRope.AltitudeFor(), StrangeWalkerMat);
        }
    }

    private static readonly string StrangeWalkerTexPath = "UI/Overlays/StrangeWalker";
    private static readonly Material StrangeWalkerMat = MaterialPool.MatFrom(StrangeWalkerTexPath, ShaderDatabase.Transparent, GenColor.FromBytes(151, 34, 34));


    public List<Tentacle> targets = [];

    public CompProperties_StrangeWalker Props => (CompProperties_StrangeWalker)props;

    public override void CompTick()
    {

        if (targets.NullOrEmpty() || targets.Count < Props.tentacleCount)
        {
            InitTentacles();
        }

        foreach (Tentacle tentacle in targets)
        {
            tentacle.Tick();
        }
    }

    public virtual void InitTentacles()
    {
        if (targets.NullOrEmpty()) targets = [];

        var targetsToCreate = Props.tentacleCount - targets.Count;
        for (int i = 0; i < targetsToCreate; i++)
        {
            targets.Add(new Tentacle(parent as Pawn, Props.searchRadius, Props.maxLength));
        }
    }

    public IntVec3 nextCell = IntVec3.Invalid;


    public override void PostDraw()
    {
        base.PostDraw();
        foreach (Tentacle target in targets)
        {
            target.Draw();
        }
    }

}
