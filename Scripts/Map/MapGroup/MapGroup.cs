using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TeamTag
{
    White,
    Red
}

[System.Serializable]
public class MapGroup
{
    [SerializeField]
    private List<Unit> _units = new List<Unit>();
    [SerializeField]
    private string _temKey;
    [SerializeField]
    private string _key;

    [SerializeField] private float _posX;
    [SerializeField] private float _posY;

    public List<Unit> Units => _units;
    public string TeamKey => _temKey;
    public string Key => _key;

    public Vector2 Pos => new Vector2(_posX, _posY);

    public bool IsDefeated()
    {
        foreach (var unit in _units)
        {
            if (!unit.IsDead)
                return false;
        }

        return true;
    }

    public void SetPos(float posX, float posY)
    {
        _posX = posX;
        _posY = posY;
    }

    public void RemoveDead()
    {
        _units.RemoveAll(x => x.IsDead);
    }

    public void UpdateWithGroup(MapGroup group)
    {
        _units = group._units;
    }
}
