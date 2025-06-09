using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Building
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private UnitType _unitType;

    [SerializeField] private int _unitsCount;
    [SerializeField] private int _unitsGrow;

    public int UnitsCount => _unitsCount;
    public UnitType UnitType => _unitType;
    public string Name => _name;

    public void Hire(MapGroup mapGroup)
    {
        if (_unitsCount <= 0)
            return;

        Unit unit = Unit.GetUnit(UnitType);
        unit.SetTeam(mapGroup);

        mapGroup.Units.Add(unit);

        _unitsCount--;
    }
}
