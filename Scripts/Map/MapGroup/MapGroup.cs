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
    private string _key;

    [SerializeField] private float _posX;
    [SerializeField] private float _posY;

    public List<Unit> Units => _units;
    public string Key => _key;

    public Vector2 Pos => new Vector2(_posX, _posY);

    public void SetPos(float posX, float posY)
    {
        _posX = posX;
        _posY = posY;
    }
}
