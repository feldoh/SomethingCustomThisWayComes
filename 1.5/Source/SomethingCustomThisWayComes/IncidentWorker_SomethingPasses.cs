using RimWorld;
using UnityEngine;
using Verse;

namespace SomethingCustomThisWayComes
{
  public class IncidentWorker_SomethingPasses : IncidentWorker
  {
    private const int PawnsStayDurationMin = 90000;
    private const int PawnsStayDurationMax = 150000;

    protected override bool CanFireNowSub(IncidentParms parms)
    {
      var pawnKind = parms.pawnKind ?? def.pawnKind;
      if (pawnKind == null || !base.CanFireNowSub(parms)) return false;
      var target = (Map)parms.target;
      return target.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(pawnKind.race) &&
             TryFindEntryCell(target, out _);
    }

    protected override bool TryExecuteWorker(IncidentParms parms)
    {
      var pawnKind = parms.pawnKind ?? def.pawnKind;
      var target = (Map)parms.target;
      if (pawnKind == null || !TryFindEntryCell(target, out var entryCell))
        return false;
      var numberToWanderIn = Mathf.Clamp(
        GenMath.RoundRandom(StorytellerUtility.DefaultThreatPointsNow(target) / pawnKind.combatPower),
        2, Rand.RangeInclusive(3, 7));
      var lurkTime = Rand.RangeInclusive(PawnsStayDurationMin, PawnsStayDurationMax);
      if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(entryCell, target, 10f, out var location))
        location = IntVec3.Invalid;
      var labelPlural = pawnKind.GetLabelPlural(numberToWanderIn);
      Log.Message($"{numberToWanderIn} {labelPlural} will hang around for {lurkTime} ticks at {location}");
      var newPawn = (Pawn)null;
      for (var index = 0; index < numberToWanderIn; ++index)
      {
        var loc = CellFinder.RandomClosewalkCellNear(entryCell, target, 10);
        newPawn = PawnGenerator.GeneratePawn(pawnKind);
        GenSpawn.Spawn(newPawn, loc, target, Rot4.Random);
        newPawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + lurkTime;
        if (location.IsValid)
          newPawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(location, target, 10);
      }

      SendStandardLetter(
        "Taggerung_LetterLabelSomethingPasses".Translate((NamedArgument)labelPlural).CapitalizeFirst(),
        "Taggerung_LetterSomethingPasses".Translate((NamedArgument)pawnKind.label), LetterDefOf.PositiveEvent, parms,
        (Thing)newPawn);
      return true;
    }

    private static bool TryFindEntryCell(Map map, out IntVec3 entryCell) =>
      RCellFinder.TryFindRandomPawnEntryCell(out entryCell, map, CellFinder.EdgeRoadChance_Animal + 0.2f);
  }
}
