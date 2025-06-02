using UnityEngine;

[CreateAssetMenu(fileName = "UnitsLib", menuName = "Scriptable Objects/UnitsLib")]
public class UnitsLib : ScriptableObject
{
    [SerializeField] private UnitController _zombie;
    [SerializeField] private UnitController _giant;
    [SerializeField] private UnitController _mage;

    public UnitController GetUnitPrefab(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Zombie:
                return _zombie;

            case UnitType.Giant:
                return _giant;

            case UnitType.Mage:
                return _mage;
        }

        return _zombie;
    }
}
