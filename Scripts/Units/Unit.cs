using UnityEngine;

[System.Serializable]
public class Unit
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;

    [SerializeField] private string _teamKey;

    public float Health { get => _health; }
    public float MaxHealth { get => _maxHealth; }
    public float Damage { get => _damage; }

    public string TeamKey { get => _teamKey; }

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
}
