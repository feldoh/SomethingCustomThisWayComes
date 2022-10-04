# Something Custom This Way Comes

A very basic library mod which simply adds some more configurable versions of basic something wanders in events.
Imagine the base game Thrumbo event but not just Thrumbos!

## Usage
As a player you won't use this directly it's just a base for other mods.

### From XML
Simply make an incident like any other. As a minimum this needs an IncidentDef in your Defs folder e.g. `MyCoolMod/Defs/Storyteller/Incidents_Map_Misc.xml`
That, if you want to be visited by Muffalo, looks something like this:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<Defs>
  <IncidentDef>
    <!-- Remember to always prefix your defs! -->
    <defName>Taggerung_TestIncident1</defName>
    <label>Test Incident 1</label>
    <category>Misc</category>
    <targetTags>
      <li>Map_PlayerHome</li>
    </targetTags>
    <baseChance>0.7</baseChance>
    <minRefireDays>13</minRefireDays>
    <allowedBiomes>
      <li>TropicalRainforest</li>
    </allowedBiomes>

    <!-- Select the defName of the type of entity you want to come to visit -->
	<pawnKind>Muffalo</pawnKind>
    <!-- Make sure to select the special incident worker-->
    <workerClass>SomethingCustomThisWayComes.IncidentWorker_SomethingPasses</workerClass>
  </IncidentDef>
</Defs>
```

You can have as many incidents as you like as long as they all have a unique `defName`

## Thanks
* Ludeon for such a great game with excellent mod support.
* Marnador for the RimWorld font
