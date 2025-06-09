using UnityEngine;

public enum UnitType
{
    Zombie,
    Giant,
    Mage
}

[System.Serializable]
public class Unit
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;

    [SerializeField] private string _teamKey;

    [SerializeField] private UnitType _unitType;

    public float Health { get => _health; }
    public float MaxHealth { get => _maxHealth; }
    public float Damage { get => _damage; }

    public string TeamKey { get => _teamKey; }
    public UnitType UnitType => _unitType;

    public bool IsDead => _health <= 0;

    public bool IsEnemy(Unit unit) 
        => _teamKey != unit.TeamKey;

    public void TakeDamage(Unit unit)
    {
        _health -= unit.Damage;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public void SetTeam(MapGroup group)
    {
        _teamKey = group.Key;
    }

    public static Unit GetUnit(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Zombie:
                return new Unit { _damage = 3, _health = 8, _maxHealth = 8, _unitType = UnitType.Zombie };
            case UnitType.Giant:
                return new Unit { _damage = 5, _health = 24, _maxHealth = 24, _unitType = UnitType.Giant };
        }

        return new Unit { _damage = 3, _health = 8, _maxHealth = 8, _unitType = UnitType.Zombie };
    }
}
