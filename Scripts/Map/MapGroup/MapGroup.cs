using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MapGroup
{
    [SerializeField]
    private List<Unit> _units = new List<Unit>();
    [SerializeField]
    private string _key;

    [SerializeField] private int _posX;
    [SerializeField] private int _posY;

    public List<Unit> Units => _units;
    public string Key => _key;

    public Vector2 vector2 => new Vector2(_posX, _posY);

    public void SetPos(int posX, int posY)
    {
        _posX = posX;
        _posY = posY;
    }
}
