using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MapGroupProcessor
{
    public static (UnitType unitType, int count)[] GetUnitsWithCount(MapGroup mapGroup)
    {
        var unitCounts = new Dictionary<UnitType, int>();

        foreach (var unit in mapGroup.Units)
        {
            if (unitCounts.ContainsKey(unit.UnitType))
                unitCounts[unit.UnitType]++;
            else
                unitCounts[unit.UnitType] = 1;
        }

        return unitCounts
            .Select(kvp => (kvp.Key, kvp.Value))
            .ToArray();
    }

    public static void InitUnitsTeam(MapGroup mapGroup)
    {
        foreach (var unit in mapGroup.Units)
        {
            unit.SetTeam(mapGroup);
        }
    }
}
