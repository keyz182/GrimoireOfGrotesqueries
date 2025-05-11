using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace GrimoireOfGrotesqueries;

public class ShovingPawnFlyer: PawnFlyer
{
    public List<Thing> shovedThings = new();
    public FloatRange DamageRange = new(12.5f, 25f);

    public virtual void Shove(Thing thing)
    {
        shovedThings.Add(thing);
        if (thing is Pawn pawn)
        {
            pawn.TakeDamage(new DamageInfo(DamageDefOf.Crush, DamageRange.RandomInRange, 1, instigator: this));
            Vector3 vect = ( pawn.DrawPos - DrawPos) * 7;
            IntVec3 end = pawn.Position + vect.ToIntVec3();

            FleckMaker.ThrowDustPuffThick(pawn.Position.ToVector3Shifted(), Map, 5f, Color.grey);
            SoundDefOf.Pawn_Melee_Punch_HitPawn.PlayOneShot(SoundInfo.InMap(pawn));

            DoJump(pawn, end.ClampInsideMap(Map));

        }
        else
        {
            thing.TakeDamage(new DamageInfo(DamageDefOf.Crush, DamageRange.RandomInRange, 1, instigator: this));
        }
    }

    public virtual bool DoJump(
        Pawn pawn,
        LocalTargetInfo currentTarget)
    {
        IntVec3 position = pawn.Position;
        IntVec3 cell = currentTarget.Cell;
        Map map = pawn.Map;
        bool isSelected = Find.Selector.IsSelected(pawn);
        PawnFlyer newThing = MakeFlyer(ThingDefOf.PawnFlyer_Stun, pawn, cell, DefDatabase<EffecterDef>.GetNamed("JumpFlightEffect"), SoundDefOf.Pawn_Melee_Punch_HitBuilding_Generic, flyWithCarriedThing: true);
        if (newThing == null)
            return false;
        FleckMaker.ThrowDustPuff(position.ToVector3Shifted() + Gen.RandomHorizontalVector(0.5f), map, 2f);
        GenSpawn.Spawn(newThing, cell, map);
        if (isSelected)
            Find.Selector.Select(pawn, false, false);
        return true;
    }

    public override void Tick()
    {
        base.Tick();

        if(Map == null) return;

        if (Destroyed) return;

        IEnumerable<Thing> things = GenRadial.RadialCellsAround(Position, 2, true).SelectMany(c => Map.thingGrid.ThingsAt(c)).Where(t => !shovedThings.Contains(t) && t.def != ThingDefOf.PawnFlyer_Stun);

        foreach (Thing thing in things)
        {
            Shove(thing);
        }

    }


    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref shovedThings, "shovedThings", LookMode.Reference);
    }
}
